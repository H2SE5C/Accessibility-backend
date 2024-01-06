using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend;
using Accessibility_backend.Modellen;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErvaringsdeskundigeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly IEmailSender _emailSender;
        public ErvaringsdeskundigeController(UserManager<Gebruiker> userManager, ApplicationDbContext context, IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        // GET: api/<ErvaringsdeskundigeController>
        [HttpGet]
        public async Task<IActionResult> GetErvaringsdeskundigen()
        {
            var ervaringsdeskundigen = _context.Ervaringsdeskundigen
              .Include(e => e.Aandoeningen)
              .ToList();

            var ervaringsdeskundigenDto = ervaringsdeskundigen
                .Select(e => new ErvaringsdeskundigeDto
                {
                    Id = e.Id,
                    Aandoeningen = e.Aandoeningen.Select(a => new AandoeningDto
                    {
                        Id = a.Id,
                        Naam = a.Naam
                    }).ToList()
                })
                .ToList();

            return Ok(ervaringsdeskundigenDto);
        }
        //dit kan ergens anders zodat bedrijf het ook kan gebruiken misschien? idk
        [HttpGet("/verifieer")]
        public async Task<IActionResult> VerifieerEmail(string token, string email)
        {
            var gebruiker = await _userManager.FindByEmailAsync(email);
            if (gebruiker == null)
                return BadRequest();

            await _userManager.ConfirmEmailAsync(gebruiker, token);
            return Ok("Geverifieerd! U kan dit venster sluiten.");
        }

        // GET api/<ErvaringsdeskundigeController>/5
        //dit is uhhh de oplossing voor de many to many??
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ervaringsdeskundige = await _context.Ervaringsdeskundigen
        .Where(e => e.Id == id)
        .Include(e => e.Aandoeningen)
        .Include(e => e.Hulpmiddelen)
        .Select(e => new ErvaringsdeskundigeDto
        {
            Id = e.Id,
            Voornaam = e.Voornaam,
            Achternaam = e.Achternaam,
            Postcode = e.Postcode,
            Minderjarig = e.Minderjarig,
            Hulpmiddelen = e.Hulpmiddelen.Select(a => new HulpmiddelDto
            {
                Id = a.Id,
                Naam = a.Naam
            }).ToList(),
            Aandoeningen = e.Aandoeningen.Select(a => new AandoeningDto
            {
                Id = a.Id,
                Naam = a.Naam
            }).ToList(),
            /*Commerciële = e.Commerciële,*/
            Voogd = e.Voogd
        })
        .FirstAsync();

            if (ervaringsdeskundige != null)
            {
                return Ok(ervaringsdeskundige);
            }

            return NotFound();
        }

        // POST api/<ErvaringsdeskundigeController>
        //voorbeeld:
        /*{
			"voornaam": "Dude",
			"achternaam": "Awesome",
			"wachtwoord": "String123@",
			"email": "Killer@example.com",
			"postcode": "2224GE",
			"minderjarig": false,
			"telefoonnummer": "0684406262",
			"aandoeningen" : [
			{
			      "id": 2,
			      "naam": "Slechtziendheid"
			},
		    {
			      "id": 4,
			     "naam": "ADHD"
			}
			],
			"typeOnderzoeken": [
			{
				"id": 1,
				"naam": "Vragenlijst"
			}
			],
			"voorkeurBenadering": "geen voorkeur",
			"commerciële": false
			}*/
        [HttpPost]
        public async Task<IActionResult> RegistreerErvaringsdeskundige([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            var rol = await _context.Rollen.Where(r => r.Naam == "Ervaringsdeskundige").FirstAsync();
            var Hulpmiddelen = _context.Hulpmiddelen.Where(a => model.Hulpmiddelen.Select(aa => aa.Id).Contains(a.Id)).ToList();
            var Aandoeningen = _context.Aandoeningen.Where(a => model.Aandoeningen.Select(aa => aa.Id).Contains(a.Id)).ToList();
            var TypeOnderzoeken = _context.TypeOnderzoeken.Where(t => model.TypeOnderzoeken.Select(to => to.Id).Contains(t.Id)).ToList();

            Voogd Voogd = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            if (model.Minderjarig)
            {
                Voogd = await _context.Voogden.Where(v => v.Email == model.VoogdEmail).FirstOrDefaultAsync();
                if (Voogd == null)
                {
                    Voogd = new()
                    {
                        Voornaam = model.VoogdVoornaam,
                        Achternaam = model.VoogdAchternaam,
                        Email = model.VoogdEmail,
                        Telefoonnummer = model.VoogdTelefoonnummer
                    };

                    await _context.Voogden.AddAsync(Voogd);
                    await _context.SaveChangesAsync();
                }
            }
            Ervaringsdeskundige ervaringsdeskundige = new()
            {
                Voornaam = model.Voornaam,
                Achternaam = model.Achternaam,
                Postcode = model.Postcode,
                Minderjarig = model.Minderjarig,
                PhoneNumber = model.Telefoonnummer,
                Hulpmiddelen = Hulpmiddelen,
                Aandoeningen = Aandoeningen,
                VoorkeurBenadering = model.VoorkeurBenadering,
                TypeOnderzoeken = TypeOnderzoeken,
                UserName = model.Email,
				Commerciële = model.Commerciële,
                Email = model.Email,
                Rol = rol,
                Voogd = Voogd
            };

            var result = await _userManager.CreateAsync(ervaringsdeskundige, model.Wachtwoord);
            if (!result.Succeeded)
            {
                var exceptionText = result.Errors.Aggregate("User Creation Failed - Identity Exception. Errors were: \n\r\n\r", (current, error) => current + (" - " + error + "\n\r"));
                throw new Exception(exceptionText);
                /*return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });*/
            }

            //email verzend stuk kan ook misschien een methode worden?
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(ervaringsdeskundige);
            var link = Url.Action(nameof(VerifieerEmail), "Ervaringsdeskundige", new { token, email = ervaringsdeskundige.Email }, Request.Scheme);
            await _emailSender.SendEmailAsync(ervaringsdeskundige.Email, "verifieer email accessibility", link);

            /*	await _userManager.AddToRoleAsync(ervaringsdeskundige, "Ervaringsdeskundige");*/
            return Ok(new Response { Status = "Success", Message = "Er is een verificatie email verstuurd naar: " + ervaringsdeskundige.Email + "!" });
        }

        // PUT api/<ErvaringsdeskundigeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ErvaringsdeskundigeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
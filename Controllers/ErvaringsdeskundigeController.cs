using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErvaringsdeskundigeController : ControllerBase
    {
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Gebruiker> _userManager;
		public ErvaringsdeskundigeController(UserManager<Gebruiker> userManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		// GET: api/<ErvaringsdeskundigeController>
		[HttpGet]
		public IActionResult GetErvaringsdeskundigen()
		{
			return Ok(_context.Ervaringsdeskundigen.ToList());
		}

		// GET api/<ErvaringsdeskundigeController>/5
		//het pakken van relationeel data zoals aandoeningen kan nog niet... misschien moet ik tussentabel maken om het werkend te maken?
		//de many to many tabel is wel gevuld in de database, ik kan het gewoon niet ophalen door de structuur van de modellen
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var gebruiker = _context.Ervaringsdeskundigen
		.Include(e => e.Aandoeningen) 
		.FirstOrDefault(e => e.Id == id);

			if (gebruiker != null)
			{
				return Ok(gebruiker);
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
			"commercerciële": true,
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
			
			if (model.Minderjarig) {
				Voogd = await _context.Voogden.Where(v => v.Email == model.VoogdEmail).FirstOrDefaultAsync();
				if (Voogd == null) {
					Voogd = new ()
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
				Commercerciële = model.Commercerciële,
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
			return Ok(new Response { Status = "Success", Message = "User created successfully!" });
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

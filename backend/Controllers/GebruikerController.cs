using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend;
using Accessibility_backend.Modellen.DTO;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//controller wordt gebruikt om admins te maken. developer is ook een soort gebruiker maar dat maken we wel direct in DB
namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public GebruikerController(ApplicationDbContext applicationDbContext, IEmailSender emailSender)
        {
            _context = applicationDbContext;
            _emailSender = emailSender;
        }
        // GET: api/<GebruikerController>

        [HttpPut("bedrijf/{id}")]
        public async Task<IActionResult> setEmailTrue(int id)
        {
            var bedrijf = await _context.Bedrijven.FindAsync(id);
            bedrijf.EmailConfirmed = true;
            await _context.SaveChangesAsync();
            var result = await _emailSender.SendEmailAsync(bedrijf.Email, "Verifieer email - Accessibility", "Uw bedrijf account is actief");

            if (result.Status.Equals("Success"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [Authorize(Roles = "Ervaringsdeskundige")]
        [HttpGet]
        public async Task<IActionResult> GetGebruikers()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //misschien nog check als gebruikers lijst null is?
            return Ok(await _context.Gebruikers.ToListAsync());
        }

        [HttpGet("ervaringsdeskundigen")]
        public async Task<IActionResult> GetErvaringsdeskundigen()
        {
            var gebruikersMetAandoeningen = await _context.Ervaringsdeskundigen
                .Include(e => e.Aandoeningen)
                .Include(e => e.Hulpmiddelen)
                .Include(e => e.Onderzoeken)
                .ThenInclude(o => o.Bedrijf)
                
                .ToListAsync();

            var gebruikersMetAandoeningenDto = gebruikersMetAandoeningen
                .Select(e => new ErvaringsdeskundigeDto
                {
                    Id = e.Id,
                    Voornaam = e.Voornaam,
                    Achternaam = e.Achternaam,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Postcode = e.Postcode,
                    VoorkeurBenadering = e.VoorkeurBenadering,
                    Aandoeningen = e.Aandoeningen.Select(a => new AandoeningDto
                    {
                        Id = a.Id,
                        Naam = a.Naam
                    }).ToList(),
                    Hulpmiddelen = e.Hulpmiddelen.Select(a => new HulpmiddelDto
                    {
                        Id = a.Id,
                        Naam = a.Naam
                    }).ToList(),
                    Onderzoeken = e.Onderzoeken.Select(a => new OnderzoekDto
                    {
                        Id = a.Id,
                        Titel = a.Titel,
                        Bedrijf = a.Bedrijf.Bedrijfsnaam
                    }).ToList()
                })
                .ToList();

            return Ok(gebruikersMetAandoeningenDto);
        }


        [HttpGet("bedrijven")]
        public async Task<IActionResult> GetAllesBedrijf()
        {
            var bedrijven = await _context.Bedrijven.Where(b => b.EmailConfirmed == true).ToListAsync();
            var bedrijvenAfwachting = await _context.Bedrijven.Where(b => b.EmailConfirmed == false).ToListAsync();

            var response = new BedrijvenResponse
            {
                bedrijvenTrue = bedrijven,
                bedrijvenFalse = bedrijvenAfwachting,
            };
            return Ok(response);
        }
        [HttpGet("medewerkers")]
        public async Task<IActionResult> GetAllesMedewerker()
        {
            var medewerkers = await _context.Medewerkers.ToListAsync();
            return Ok(medewerkers);
        }

        // GET api/<GebruikerController>/5
        // get related data with Include(). _context.Gebruikers.Where(g => g.Id == id).Include(g => g.Rol).ToListAsync()
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var gebruiker = await _context.Gebruikers.Where(g => g.Id == id).FirstAsync();

            if (gebruiker != null)
            {
                return Ok(gebruiker);
            }

            return NotFound();
        }

        /* POST api/<GebruikerController>
        voorbeeld request: 
          {
                "email": "string",
                "wachtwoord": "string",
                "rol": "admin"
            }*/
        /*[HttpPost]
            public async Task<IActionResult> MaakGebruikerAan([FromBody] Gebruiker gebruiker)
            {
                var nieuweGebruiker = new Gebruiker()
                {
                   Email = gebruiker.Email,
                   Wachtwoord = gebruiker.Wachtwoord,
                   Geverifieerd = true,
                };

                await _context.Gebruikers.AddAsync(nieuweGebruiker);
                await _context.SaveChangesAsync();

                return Ok(nieuweGebruiker);
            }*/

        // PUT api/<GebruikerController>/5
        /* [HttpPut("{id}")]
         public async Task<IActionResult> VeranderGegevens(int id, [FromBody] Gebruiker upgedateGebruiker)
         {
             var gebruiker = await _context.Gebruikers.FindAsync(id);

             if (gebruiker != null) { 
                 gebruiker.Email = upgedateGebruiker.Email;
                 gebruiker.Wachtwoord = upgedateGebruiker.Wachtwoord;
                 await _context.SaveChangesAsync();
                 return Ok(gebruiker);
             }

             return NotFound();
         }
 */
        // DELETE api/<GebruikerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var gebruiker = await _context.Gebruikers.FindAsync(id);
                _context.Remove(gebruiker);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("niet gelukkig");
            }

            return NotFound();
        }
    }
}

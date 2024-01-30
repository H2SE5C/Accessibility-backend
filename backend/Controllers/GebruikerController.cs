using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend;
using Accessibility_backend.Modellen.DTO;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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
            var bedrijvenTrue = await _context.Bedrijven
                .Include(b => b.BedrijfsOnderzoekslijst)
                .Where(b => b.EmailConfirmed == true)
                .Select(b => new BedrijfDto
                {
                    Id = b.Id,
                    Bedrijfsnaam = b.Bedrijfsnaam,
                    Omschrijving = b.Omschrijving,
                    Email = b.Email,
                    EmailConfirmed = b.EmailConfirmed,
                    PhoneNumber = b.PhoneNumber,
                    Locatie = b.Locatie,
                    LinkNaarBedrijf = b.LinkNaarBedrijf,
                    BedrijfsOnderzoekslijst = b.BedrijfsOnderzoekslijst.Select(o => new OnderzoekDto
                    {
                        Id = o.Id,
                        Titel = o.Titel,
                        Omschrijving = o.Omschrijving,
                    }).ToList()
                })
                .ToListAsync();

            var bedrijvenFalse = await _context.Bedrijven
                .Include(b => b.BedrijfsOnderzoekslijst)
                .Where(b => b.EmailConfirmed == false)
                .Select(b => new BedrijfDto
                {
                    Id = b.Id,
                    Bedrijfsnaam = b.Bedrijfsnaam,
                    Omschrijving = b.Omschrijving,
                    Email = b.Email,
                    EmailConfirmed = b.EmailConfirmed,
                    PhoneNumber = b.PhoneNumber,
                    Locatie = b.Locatie,
                    LinkNaarBedrijf = b.LinkNaarBedrijf,
                    BedrijfsOnderzoekslijst = b.BedrijfsOnderzoekslijst.Select(o => new OnderzoekDto
                    {
                        Id = o.Id,
                        Titel = o.Titel,
                        Omschrijving = o.Omschrijving,
                    }).ToList()
                })
                .ToListAsync();

            var response = new BedrijvenResponse
            {
                bedrijvenTrue = bedrijvenTrue,
                bedrijvenFalse = bedrijvenFalse,
            };
            return Ok(response);
        }

        [HttpGet("onderzoeken")]
        public async Task<IActionResult> GetOnderzoekenStatus()
        {
            var onderzoeken = new ArrayList();
            var onderzoekenGoedgekeurd = await _context.Onderzoeken
                .Include(o => o.Bedrijf)
                .Include(o => o.TypeOnderzoek)
                .Where(o => o.Status != "In afwachting")
                .Select(o => new OnderzoekDto
                {
                    Id = o.Id,
                    Titel = o.Titel,
                    Omschrijving = o.Omschrijving,
                    Vragenlijst = o.Vragenlijst.Id,
                    Beloning = o.Beloning,
                    Status = o.Status,
                    Bedrijf = o.Bedrijf.Bedrijfsnaam,
                    Datum = o.Datum,
                    Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
                    {
                        Id = e.Id,
                        Email = e.Email,
                    }).ToList(),
                    Beperkingen = o.Beperkingen.Select(b => new BeperkingDto
                    {
                        Id = b.Id,
                        Naam = b.Naam
                    }).ToList(),
                    TypeOnderzoek = o.TypeOnderzoek.Naam
                })
                .ToListAsync();

            var onderzoekenAnderen = await _context.Onderzoeken
                .Include(o => o.Bedrijf)
                .Include(o => o.TypeOnderzoek)
                .Where(o => o.Status == "In afwachting")
                .Select(o => new OnderzoekDto
                {
                    Id = o.Id,
                    Titel = o.Titel,
                    Omschrijving = o.Omschrijving,
                    Vragenlijst = o.Vragenlijst.Id,
                    Beloning = o.Beloning,
                    Status = o.Status,
                    Bedrijf = o.Bedrijf.Bedrijfsnaam,
                    Datum = o.Datum,
                    Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
                    {
                        Id = e.Id,
                        Email = e.Email,
                    }).ToList(),
                    Beperkingen = o.Beperkingen.Select(b => new BeperkingDto
                    {
                        Id = b.Id,
                        Naam = b.Naam
                    }).ToList(),
                    TypeOnderzoek = o.TypeOnderzoek.Naam
                })
                .ToListAsync();

            var response = new OnderzoekenResponse
            {
                onderzoekenEerste = onderzoekenGoedgekeurd,
                onderzoekenTweede = onderzoekenAnderen
            };


            return Ok(response);
        }

        [HttpGet("onderzoeken/{id}")]
        public async Task<IActionResult> GetOnderzoekId(int id)
        {
            var onderzoek = await _context.Onderzoeken
               .Include(o => o.Bedrijf)
               .Include(o => o.TypeOnderzoek)
               .Where(o => o.Id == id)
               .Select(o => new OnderzoekDto
               {
                   Id = o.Id,
                   Titel = o.Titel,
                   Omschrijving = o.Omschrijving,
                   Vragenlijst = o.Vragenlijst.Id,
                   Beloning = o.Beloning,
                   Status = o.Status,
                   Bedrijf = o.Bedrijf.Bedrijfsnaam,
                   Datum = o.Datum,
                   Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
                   {
                       Id = e.Id,
                       Email = e.Email,
                   }).ToList(),
                   Beperkingen = o.Beperkingen.Select(b => new BeperkingDto
                   {
                       Id = b.Id,
                       Naam = b.Naam
                   }).ToList(),
                   TypeOnderzoek = o.TypeOnderzoek.Naam
               })
               .FirstAsync();

            return Ok(onderzoek);
        }

        [HttpPut("onderzoeken/AkkoordStatus/{id}")]
        public async Task<IActionResult> AkkordStatus(int id)
        {
            var onderzoek = await _context.Onderzoeken.FindAsync(id);
            onderzoek.Status = "Actief";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPut("onderzoeken/NietAkkoordStatus/{id}")]
        public async Task<IActionResult> NietAkkordStatus(int id)
        {
            var onderzoek = await _context.Onderzoeken.FindAsync(id);
            onderzoek.Status = "Afgekeurd";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("onderzoeken/verwijderen/{id}")]
        public async Task<IActionResult> VerwijderOnderzoek(int id)
        {
            try
            {
                var onderzoek = await _context.Onderzoeken.FindAsync(id);
                _context.Onderzoeken.Remove(onderzoek);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("niet gelukkig");
            }
            return Ok(id);
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

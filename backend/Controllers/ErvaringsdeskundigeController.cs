using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend;
using Accessibility_backend.Modellen;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetErvaringsdeskundigen()
        {
            var ervaringsdeskundigen = await _context.Ervaringsdeskundigen
              .Include(e => e.Aandoeningen)
              .ToListAsync();

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


        [HttpGet("Aandoeningen")]
        public async Task<IActionResult> GetAandoeningen()
        {
            var aandoeningen = await _context.Aandoeningen.ToListAsync();

            return Ok(aandoeningen);
        }

        [HttpGet("TypeOnderzoeken")]
        public async Task<IActionResult> GetTypeOnderzoeken()
        {
            var typeOnderzoeken = await _context.TypeOnderzoeken.ToListAsync();

            return Ok(typeOnderzoeken);
        }

		[HttpGet("Hulpmiddelen")]
		public async Task<IActionResult> GetHulpmiddelen()
		{
			var hulpmiddelen = await _context.Hulpmiddelen.ToListAsync();

			return Ok(hulpmiddelen);
		}

        // PUT api/ervaringsdeskundige/update
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> PutErvaringsdeskundige([FromBody] ErvaringsdeskundigeDto ervaringsdeskundigeUpdates)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ervaringsdeskundige = await _context.Ervaringsdeskundigen.FirstOrDefaultAsync(b => b.Id == int.Parse(userId));

            if (ervaringsdeskundige == null)
            {
                return NotFound();
            }

            // Werk alleen de velden bij die zijn gewijzigd
            ervaringsdeskundige.Voornaam = ervaringsdeskundigeUpdates.Voornaam;
            ervaringsdeskundige.Achternaam = ervaringsdeskundigeUpdates.Achternaam;

            //ervaringsdeskundige.Email = ervaringsdeskundigeUpdates.Email;
            ervaringsdeskundige.Postcode = ervaringsdeskundigeUpdates.Postcode;
            //ervaringsdeskundige.PhoneNumber = ervaringsdeskundigeUpdates.PhoneNumber;
            ervaringsdeskundige.Commerciële = ervaringsdeskundigeUpdates.Commercerciële;
            ervaringsdeskundige.Minderjarig = ervaringsdeskundige.Minderjarig;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(ervaringsdeskundige); // Geef bijgewerkte gegevens terug als succesvol
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Er is een fout opgetreden bij het bijwerken van het profiel.");
            }
        }

        // DELETE api/ervaringsdeskundige/delete
        [Authorize]
        [HttpDelete("delete-profiel")]
        public async Task<IActionResult> DeleteErvaringsdeskundige()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ervaringsdeskundige = await _context.Ervaringsdeskundigen.FirstOrDefaultAsync(b => b.Id == int.Parse(userId));

            _context.Ervaringsdeskundigen.Remove(ervaringsdeskundige);
            await _context.SaveChangesAsync();

            return NoContent();
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

        // GET api/<ErvaringsdeskundigeController>
        [Authorize]
        [HttpGet("profiel")]
        public async Task<IActionResult> GetErvaringsdeskundige() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ervaringsdeskundige = await _context.Ervaringsdeskundigen.FirstOrDefaultAsync(b => b.Id == int.Parse(userId));
            return Ok(ervaringsdeskundige);
        }
    }
}
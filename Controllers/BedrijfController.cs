using Accessibility_app.Data;
using Accessibility_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Accessibility_backend.Modellen.DTO;

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedrijfController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BedrijfController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<BedrijfController>
        [HttpGet("lijst")]
        public async Task<IActionResult> GetBedrijven()
        {
            var bedrijven = await _context.Bedrijven.ToListAsync();
            return Ok(bedrijven);
        }

        // GET api/<BedrijfController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bedrijf = await _context.Bedrijven.FirstOrDefaultAsync(b => b.Id == id);
            return Ok(bedrijf);
        }

        // GET: api/<BedrijfController> (pakt eigen gegevens van bedrijf)
        [Authorize]
        [HttpGet("profiel")]
        public async Task<IActionResult> GetBedrijf()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var bedrijf = await _context.Bedrijven.FirstOrDefaultAsync(b => b.Id == int.Parse(userId));
            return Ok(bedrijf);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> PutBedrijf([FromBody] BedrijfDto bedrijfUpdates)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var bedrijf = await _context.Bedrijven.FirstOrDefaultAsync(b => b.Id == int.Parse(userId));

            if (bedrijf == null)
            {
                return NotFound();
            }

            // Werk alleen de velden bij die zijn gewijzigd
            bedrijf.Bedrijfsnaam = bedrijfUpdates.Bedrijfsnaam;
            bedrijf.Email = bedrijfUpdates.Email;
            bedrijf.Locatie = bedrijfUpdates.Locatie;
            bedrijf.Omschrijving = bedrijfUpdates.Omschrijving;
            bedrijf.PhoneNumber = bedrijfUpdates.PhoneNumber;
            bedrijf.LinkNaarBedrijf = bedrijfUpdates.LinkNaarBedrijf;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(bedrijf); // Geef bijgewerkte gegevens terug als succesvol
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Er is een fout opgetreden bij het bijwerken van het bedrijf.");
            }
        }

        // DELETE api/<BedrijfController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Implementeer hier de logica om een bedrijf te verwijderen
        }

        // DELETE api/Bedrijf/
        [Authorize]
        [HttpDelete("delete-profiel")]
        public async Task<IActionResult> DeleteBedrijf()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var bedrijf = await _context.Bedrijven.FirstOrDefaultAsync(b => b.Id == int.Parse(userId));

            _context.Bedrijven.Remove(bedrijf);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using Accessibility_app.Data;
using Accessibility_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedrijfController : ControllerBase
    {
		private readonly ApplicationDbContext _context;
		public BedrijfController(ApplicationDbContext context) { 
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

        // PUT: api/<BedrijfController>/profiel
        [Authorize]
        [HttpPut("profiel")]
        public async Task<IActionResult> PutBedrijf([FromBody] Bedrijf bedrijfUpdates)
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

            // bedrijf.Password = bedrijfUpdates.Password;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(bedrijf); // Geef bijgewerkte gegevens terug als succesvol
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bedrijven.Any(b => b.Id == int.Parse(userId)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }






        // DELETE api/<BedrijfController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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

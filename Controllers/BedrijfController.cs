using Accessibility_app.Data;
using Accessibility_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Accessibility_backend.Modellen.DTO;
using Microsoft.AspNetCore.Identity;

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedrijfController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Bedrijf> _userManager;

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

            // Controleer of het huidige wachtwoord correct is
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(bedrijf, bedrijfUpdates.CurrentPassword);

            if (!isCurrentPasswordValid)
            {
                return BadRequest("Het huidige wachtwoord is niet correct.");
            }

            // Controleer of de email is gewijzigd
            if (!string.IsNullOrEmpty(bedrijfUpdates.Email) && bedrijfUpdates.Email != bedrijf.Email)
            {
                // Update de username (email) en normalisatie
                bedrijf.Email = bedrijfUpdates.Email;
                bedrijf.UserName = bedrijfUpdates.Email; // Als username gelijk is aan email
                bedrijf.NormalizedEmail = bedrijfUpdates.Email.ToUpper();
                bedrijf.NormalizedUserName = bedrijfUpdates.Email.ToUpper(); // Normaliseer de username
            }

            if (!string.IsNullOrEmpty(bedrijfUpdates.Bedrijfsnaam))
            {
                bedrijf.Bedrijfsnaam = bedrijfUpdates.Bedrijfsnaam;
            }

            if (!string.IsNullOrEmpty(bedrijfUpdates.Locatie) && bedrijfUpdates.Locatie != bedrijf.Locatie)
            {
                bedrijf.Locatie = bedrijfUpdates.Locatie;
            }

            if (!string.IsNullOrEmpty(bedrijfUpdates.Omschrijving) && bedrijfUpdates.Omschrijving != bedrijf.Omschrijving)
            {
                bedrijf.Omschrijving = bedrijfUpdates.Omschrijving;
            }

            if (!string.IsNullOrEmpty(bedrijfUpdates.PhoneNumber) && bedrijfUpdates.PhoneNumber != bedrijf.PhoneNumber)
            {
                bedrijf.PhoneNumber = bedrijfUpdates.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(bedrijfUpdates.LinkNaarBedrijf) && bedrijfUpdates.LinkNaarBedrijf != bedrijf.LinkNaarBedrijf)
            {
                bedrijf.LinkNaarBedrijf = bedrijfUpdates.LinkNaarBedrijf;
            }

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

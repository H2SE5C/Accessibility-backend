using Accessibility_app.Data;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accessibility_app.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VragenlijstController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public VragenlijstController(ApplicationDbContext applicationDbContext) {
            _context = applicationDbContext;
        }

        // GET: api/<VragenlijstController>/get-all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllVragenlijsten() {
            var vragenlijsten = await _context.Vragenlijsten
                .Select(v => new VragenlijstDto {
                    Id = v.Id,
                    Naam = v.Naam
                })
                .ToListAsync();

            return Ok(vragenlijsten);
        }

        // GET: api/<VragenlijstController>/details
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetVragenlijstDetails(int id) {
            var vragenlijst = await _context.Vragenlijsten
                .FirstOrDefaultAsync(v => v.Id == id);

            return vragenlijst == null ? NotFound() : Ok(vragenlijst);
        }

        // UPDATE api/<VragenlijstController>/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVragenlijst(int id, [FromBody] VragenlijstForm vragenlijstUpdates) {
            var vragenlijst = await _context.Vragenlijsten
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vragenlijst == null) { return NotFound(); }

            vragenlijst.Naam = vragenlijstUpdates.Naam;

            try {
                await _context.SaveChangesAsync();
                return NoContent();
            } catch (DbUpdateConcurrencyException) {
                return BadRequest("Er is een fout opgetreden bij het bijwerken van de vragenlijst.");
            }
        }

        // DELETE api/<VragenlijstController>/5
        [HttpDelete("verwijderen/{id}")]
        public async Task<IActionResult> VerwijderVragenlijst(int id) {
            try {
                var vragenlijst = await _context.Vragenlijsten.FindAsync(id);
                _context.Vragenlijsten.Remove(vragenlijst);
                await _context.SaveChangesAsync();
            } catch {
                throw new Exception("niet gelukkig");
            }

            return Ok(id);
        }
    }
}

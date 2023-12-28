using Accessibility_app.Data;
using Accessibility_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GebruikerController(ApplicationDbContext applicationDbContext) { 
            _context = applicationDbContext;
        }
        // GET: api/<GebruikerController>
        [HttpGet]
        public IActionResult GetGebruikers()
        {
            return Ok(_context.Gebruikers.ToList());
        }

		// GET api/<GebruikerController>/5
		// get related data with Include(). _context.Gebruikers.Where(g => g.Id == id).Include(g => g.Rol).ToListAsync()
		[HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
			var gebruiker = _context.Gebruikers.Where(g => g.Id == id).First();

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
                "rol": "admin",
                "geverifieerd": true
            }*/
	/*[HttpPost]
        public async Task<IActionResult> MaakGebruikerAan([FromBody] Gebruiker gebruiker)
        {
            var nieuweGebruiker = new Gebruiker()
            {
               Email = gebruiker.Email,
               Wachtwoord = gebruiker.Wachtwoord,
               Rol = gebruiker.Rol,
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
			var gebruiker = await _context.Gebruikers.FindAsync(id);

			if (gebruiker != null)
			{
                _context.Remove(gebruiker);
                await _context.SaveChangesAsync();
                return Ok(gebruiker);
			}

			return NotFound();
		}
    }
}

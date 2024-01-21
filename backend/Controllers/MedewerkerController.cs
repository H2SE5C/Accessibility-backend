using Accessibility_app.Data;
using Accessibility_app.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedewerkerController : ControllerBase
    {
		private readonly ApplicationDbContext _context;

		public MedewerkerController(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		// GET: api/<MedewerkerController>
		[HttpGet]
        public IActionResult GetMedewerkers()
        {
            return Ok(_context.Medewerkers.ToList());
        }

        // GET api/<MedewerkerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

		// POST api/<MedewerkerController>
		[HttpPost]
/*		public async Task<IActionResult> MaakMedewerkerAan([FromBody] Medewerker gebruiker)
		{
			var nieuweGebruiker = new Medewerker()
			{
				Naam = gebruiker.Naam,
				Email = gebruiker.Email,
				Wachtwoord = gebruiker.Wachtwoord,
				Geverifieerd = true,
			};

			await _context.Medewerkers.AddAsync(nieuweGebruiker);
			await _context.SaveChangesAsync();

			return Ok(nieuweGebruiker);
		}*/

		// PUT api/<MedewerkerController>/5
		[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedewerkerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

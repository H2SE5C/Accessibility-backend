using Accessibility_app.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		[HttpGet]
		public async Task<IActionResult> GetBedrijven()
		{
            var bedrijven = await _context.Bedrijven.ToListAsync();
			return Ok(bedrijven);
		}

		// GET api/<BedrijfController>/5
		[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BedrijfController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BedrijfController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BedrijfController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

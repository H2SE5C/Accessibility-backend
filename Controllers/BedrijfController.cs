using Accessibility_app.Data;
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
		[HttpGet]
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

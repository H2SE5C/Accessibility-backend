using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Versioning;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnderzoekController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        // GET: api/<OnderzoekController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize(Roles = "Bedrijf")]
        // GET: api/<OnderzoekController>/Bedrijf
        [HttpGet("Bedrijf/{id}")]
        public async Task<IActionResult> GetOnderzoekBedrijf(int id)
        {
            List<Onderzoek> onderzoeks = null;
            var bedrijf = await _context.Bedrijven
              .Include(b => b.BedrijfsOnderzoekslijst)
              .FirstOrDefaultAsync(b => b.Id == id);

            if (bedrijf != null)
            {
                onderzoeks = bedrijf.BedrijfsOnderzoekslijst.ToList();
            }
            else{ throw new Exception("Bedrijf niet gevonden"); }

            return Ok(onderzoeks);
        }

        // GET api/<OnderzoekController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OnderzoekController>
        [HttpPost]
        public async void Post([FromBody] OnderzoekForm model)
        {
            var bedrijfId = model.BedrijfId;
            var bedrijf = await _context.Bedrijven.Where(b => b.Id == bedrijfId).FirstOrDefaultAsync();
            Onderzoek onderzoek = new Onderzoek() {
                Titel = model.Titel,
                Omschrijving = model.Omschrijving,
                Beloning = model.Beloning,
                Status = model.Status,
                Bedrijf = bedrijf,

            }

        }

        // PUT api/<OnderzoekController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OnderzoekForm onderzoeker)
        {

        }

        // DELETE api/<OnderzoekController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

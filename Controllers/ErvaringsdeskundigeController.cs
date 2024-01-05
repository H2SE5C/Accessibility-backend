using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend;
using Accessibility_backend.Modellen;
using Accessibility_backend.Modellen.Extra;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErvaringsdeskundigeController : ControllerBase
    {
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Gebruiker> _userManager;
		private readonly IEmailSender _emailSender;
		public ErvaringsdeskundigeController(UserManager<Gebruiker> userManager, ApplicationDbContext context, IEmailSender emailSender)
		{
			_userManager = userManager;
			_context = context;
			_emailSender = emailSender;
		}

		// GET: api/<ErvaringsdeskundigeController>
		[HttpGet]
		public async Task<IActionResult> GetErvaringsdeskundigen()
		{
			var ervaringsdeskundigen = _context.Ervaringsdeskundigen
			  .Include(e => e.Aandoeningen)
			  .ToList();

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
		//dit kan ergens anders zodat bedrijf het ook kan gebruiken misschien? idk
		[HttpGet("/verifieer")]
		public async Task<IActionResult> VerifieerEmail(string token, string email)
		{
			var gebruiker = await _userManager.FindByEmailAsync(email);
			if (gebruiker == null)
				return BadRequest();

			await _userManager.ConfirmEmailAsync(gebruiker, token);
			return Ok("Geverifieerd! U kan dit venster sluiten.");
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
			Commercerciële = e.Commercerciële,
			Voogd = e.Voogd
		})
		.FirstAsync();

			if (ervaringsdeskundige != null)
			{
				return Ok(ervaringsdeskundige);
			}

			return NotFound();
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
    }
}

using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Response = Accessibility_backend.Modellen.Registreermodellen.Response;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Gebruiker> _userManager;
		public ChatController(ApplicationDbContext context, UserManager<Gebruiker> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		// GET: api/<ChatController>
		[HttpGet]
		public async Task<IActionResult> GetChats()
		{
			var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			//misschien nog check als gebruikers lijst null is?
			return Ok(await _context.Chats.ToListAsync());
		}

		[HttpPost]
		//bij ed maak je een chat aan met 2 gebruikers 
		//bij onderzoek maak je een lege met alleen onderzoekid erbij
		//onderzoek kan knop hebben naar chat met api methode dat checkt of er al chat met de bedrijf bestaat als niet dan maakt die er een aan
		public async Task<IActionResult> MaakChat([FromBody] MaakChatAan model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new Response { Status = "Error", Message = "Vul goed in!" });
			}
			var chat = new Chat();
			try
			{
				if (model.OnderzoekId != null)
				{
					chat.OnderzoekId = model.OnderzoekId;
				}
				else if (model.Gebruiker1 != null && model.Gebruiker2 != null)
				{
					if (model.Gebruiker1 == model.Gebruiker2)
					{
						return BadRequest(new Response { Status = "Error", Message = "Zelfde gebruiker?" });
					}
					
					var gebruiker1 = await _userManager.FindByIdAsync(model.Gebruiker1.ToString());
					var gebruiker2 = await _userManager.FindByIdAsync(model.Gebruiker2.ToString());
					chat.Gebruikers = new()
					{
						gebruiker1,
						gebruiker2
					};
				}
				else
				{
					return BadRequest(new Response { Status = "Error", Message = "Lege model" });
				}
				await _context.Chats.AddAsync(chat);
				await _context.SaveChangesAsync();

				return Ok(new Response { Status = "Success", Message = "Het is gelukt" });
			}
			catch (Exception ex)
			{
				return BadRequest(new Response { Status = "Error", Message = "Fout!" });
			}
		}

		// POST api/chat/bericht
		//stap 1: check of chat bestaat met 
		[HttpPost("bericht")]
		public async Task<IActionResult> PostBericht([FromBody] MaakBerichtAan model)
		{
			var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var gebruiker = await _userManager.FindByIdAsync(id);
			if (model.ChatId != null)
			{
				var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == model.ChatId);
				if (chat != null)
				{
					var bericht = new Bericht()
					{
						Tekst = model.Tekst,
						VerzenderEmail = gebruiker.Email,
						Chat = chat
					};
					await _context.Berichten.AddAsync(bericht);
					await _context.SaveChangesAsync();

					return Ok();
				}
				return BadRequest(new Response { Status = "Error", Message = "Chat niet gevonden" });
			}
			return BadRequest(new Response { Status = "Error", Message = "Chat niet meegekregen" });
		}

		// PUT api/<ChatController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<ChatController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}

﻿using Accessibility_app.Data;
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
		public async Task<IActionResult> GetChatsVanGebruiker()
		{
			var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _context.Chats
				.Include(chat => chat.Gebruikers)
				.Where(chat => chat.Gebruikers.Any(gebruiker => gebruiker.Id == id))
				.Select(chat => new ChatDto { 
					Id = chat.Id,
					Aanmaakdatum = chat.Aanmaakdatum,
					//dit stukje pakt de andere persoon dat in chat lijstje staat
					Gebruikers = chat.Gebruikers.Where(gebruiker => gebruiker.Id != id).Select(gebruiker => new ChatGebruikerDto { 
						Id = gebruiker.Id,
						Email = gebruiker.Email
					}).ToList(),
				})
				.ToListAsync();
			//misschien nog check als gebruikers lijst null is?
			return Ok(result);
		}

		[HttpPost]
		//bij ed maak je een chat aan met 2 gebruikers 
		//bij onderzoek maak je een lege met alleen onderzoekid erbij
		public async Task<IActionResult> MaakChat([FromBody] MaakChatAan model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new Response { Status = "Error", Message = "Vul goed in!" });
			}

			var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var chatBestaatAl = _context.Chats.Where(c => c.Gebruikers.Any(u => u.Id == id) && c.Gebruikers.Any(u => u.Id == model.AnderePersoonId)).Any();

			if (chatBestaatAl) {
				return BadRequest(new Response { Status = "Error", Message = "U heeft al een chat met dit gebruiker..." });
			}
			var chat = new Chat();
			try
			{
				if (model.OnderzoekId != null)
				{
					chat.OnderzoekId = model.OnderzoekId;
				}
				else if (model.AnderePersoonId != null)
				{
					if (id == model.AnderePersoonId)
					{
						return BadRequest(new Response { Status = "Error", Message = "Zelfde gebruiker?" });
					}
					
					var gebruiker1 = await _userManager.FindByIdAsync(id.ToString());
					var gebruiker2 = await _userManager.FindByIdAsync(model.AnderePersoonId.ToString());
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

			if (gebruiker == null) {
				return BadRequest(new Response { Status = "Error", Message = "Gebruiker niet gevonden" });
			}
			if (model.ChatId != null)
			{
				var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == model.ChatId);
				if (chat != null)
				{
					var bericht = new Bericht()
					{
						Tekst = model.Tekst,
						Verzender = gebruiker,
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

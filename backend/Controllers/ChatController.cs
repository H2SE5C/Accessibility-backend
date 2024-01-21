using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Hubs;
using Accessibility_backend.Modellen.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
		private readonly IHubContext<ChatHub> _chat;
		public ChatController(ApplicationDbContext context, UserManager<Gebruiker> userManager, IHubContext<ChatHub> chat)
		{
			_context = context;
			_userManager = userManager;
			_chat = chat;
		}

		[HttpPost("[action]/{connectionId}/{chatId}")]
		public async Task<IActionResult> JoinRoom(string connectionId, int chatId)
		{
			await _chat.Groups.AddToGroupAsync(connectionId, chatId.ToString());
			return Ok();
		}
		[HttpPost("[action]/{connectionId}/{chatId}")]
		public async Task<IActionResult> LeaveRoom(string connectionId, int chatId)
		{
			await _chat.Groups.RemoveFromGroupAsync(connectionId, chatId.ToString());
			return Ok();
		}
		[HttpPost("[action]")]
		public async Task<IActionResult> SendMessage([FromBody] MaakBerichtAan model)
		{
			var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var gebruiker = await _userManager.FindByIdAsync(id);

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

				var berichtDto = new BerichtDto() { 
					Id = bericht.Id,
					Tekst = bericht.Tekst,
					Tijdstempel = bericht.Tijdstempel.ToString("yyyy-MM-dd HH:mm"),
					Verzender = new ChatGebruikerDto()
					{
						Id = gebruiker.Id,
						Email = gebruiker.Email
					}
					
				};
				await _chat.Clients.Group(model.ChatId.ToString()).SendAsync("ReceiveMessage", berichtDto);
				return Ok();
			}
			return NotFound();
		}
		// GET: api/<ChatController>
		[HttpGet]
		public async Task<IActionResult> GetChatsVanGebruiker()
		{
			var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var result = await _context.Chats
				.Include(chat => chat.Gebruikers)
				.Where(chat => chat.Gebruikers.Any(gebruiker => gebruiker.Id == id))
				.Select(chat => new ChatDto
				{
					Id = chat.Id,
					Aanmaakdatum = chat.Aanmaakdatum,
					//dit stukje pakt de andere persoon dat in chat lijstje staat
					Gebruikers = chat.Gebruikers.Where(gebruiker => gebruiker.Id != id).Select(gebruiker => new ChatGebruikerDto
					{
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

			if (chatBestaatAl)
			{
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

			if (gebruiker == null)
			{
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

		[HttpGet("bericht/{chatId}")]
		public async Task<IActionResult> GetBerichtenVanChat(int chatId)
		{
			//new 
			var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var gebruiker = await _userManager.FindByIdAsync(id);
			var chat = await _context.Chats
				.Include(chat => chat.Gebruikers)
				.Where(c => c.Id == chatId)
				.Select(c => new ChatCheck()
				{
					Id = c.Id,
					Gebruikers = c.Gebruikers.Select(g => new ChatCheckGebruiker { Id = g.Id }).ToList()
				})
				.FirstOrDefaultAsync();

			if (chat == null)
			{
				return BadRequest(new Response { Status = "Error", Message = "Geen chat gevonden voor de berichten..." });
			}
			var persoonZitInChat = chat.Gebruikers.Any(g => g.Id == int.Parse(id));

			if (!persoonZitInChat)
			{
				return Unauthorized(new Response { Status = "Error", Message = "U zit niet het chat" });
			}

			//Chatgebruiker heeft alles wat nodig is maar naam past niet echt ik wou geen dupe maken zoals 'VerzenderDto' ofzo.
			var berichten = await _context.Berichten.Where(b => b.ChatId == chat.Id)
				.Select(bericht => new BerichtDto
				{
					Id = bericht.Id,
					Tekst = bericht.Tekst,
					Tijdstempel = bericht.Tijdstempel.ToString("yyyy-MM-dd HH:mm"),
					Verzender = new ChatGebruikerDto
					{
						Id = bericht.Verzender.Id,
						Email = bericht.Verzender.Email
					}
				}
				)
				.ToListAsync();
			var gesorteerd = berichten.OrderBy(b => b.Tijdstempel);

			return Ok(gesorteerd);
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

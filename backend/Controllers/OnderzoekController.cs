using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen.DTO;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Versioning;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OnderzoekController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public OnderzoekController(ApplicationDbContext applicationDbContext) 
		{
			_context = applicationDbContext;
		}
		// GET: api/<OnderzoekController>
		[HttpGet]
		public async Task<IActionResult> GetAllOnderzoeken()
		{
			var onderzoeken = await _context.Onderzoeken
				.Include(o => o.Bedrijf)
				.Include(o=>o.TypeOnderzoek)
				.Select(o => new OnderzoekDto{
					Id = o.Id,
					Titel = o.Titel,
					Omschrijving = o.Omschrijving,
					Vragenlijst = o.Vragenlijst.Id,
					Beloning = o.Beloning,
					Status = o.Status,  
					Bedrijf = o.Bedrijf.Bedrijfsnaam,
					Datum = o.Datum,
					Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e =>new deskundigeEmailDto 
					{
						Id = e.Id,
						Email = e.Email,
					} ).ToList(),
					Beperkingen = o.Beperkingen.Select(b => new BeperkingDto { 
						Id = b.Id,
						Naam = b.Naam
					}).ToList(),
					TypeOnderzoek = o.TypeOnderzoek.Naam    
				})
				.ToListAsync();
			
			
			return Ok(onderzoeken);
		}

        [HttpGet("ervaringsdeskundige")]
        public async Task<IActionResult> GetOnderzoekenActive()
        {
            var Eid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var onderzoeken = new ArrayList();
            var onderzoekenActive = await _context.Onderzoeken
                .Include(o => o.Bedrijf)
                .Include(o => o.TypeOnderzoek)
                .Where(o => o.Status == "Actief")
                .Select(o => new OnderzoekDto
                {
                    Id = o.Id,
                    Titel = o.Titel,
                    Omschrijving = o.Omschrijving,
                    Vragenlijst = o.Vragenlijst.Id,
                    Beloning = o.Beloning,
                    Status = o.Status,
                    Bedrijf = o.Bedrijf.Bedrijfsnaam,
                    Datum = o.Datum,
                    Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
                    {
                        Id = e.Id,
                        Email = e.Email,
                    }).ToList(),
                    Beperkingen = o.Beperkingen.Select(b => new BeperkingDto
                    {
                        Id = b.Id,
                        Naam = b.Naam
                    }).ToList(),
                    TypeOnderzoek = o.TypeOnderzoek.Naam
                })
                .ToListAsync();

            var response = new OnderzoekenResponse
            {
                onderzoekenEerste = onderzoekenActive
                                    .Where(o => !o.Ervaringsdeskundigen.Any(e => e.Id == int.Parse(Eid)))
                                    .ToList(),
                onderzoekenTweede = onderzoekenActive
                                    .Where(o => o.Ervaringsdeskundigen.Any(e => e.Id == int.Parse(Eid)))
                                    .ToList(),
            };


            return Ok(response);
        }

        [HttpGet("medewerker/{id}")]
        public async Task<IActionResult> GetOnderzoekId(int id)
        {
            var onderzoek = await _context.Onderzoeken
               .Include(o => o.Bedrijf)
               .Include(o => o.TypeOnderzoek)
               .Where(o => o.Id == id)
               .Select(o => new OnderzoekDto
               {
                   Id = o.Id,
                   Titel = o.Titel,
                   Omschrijving = o.Omschrijving,
                   Vragenlijst = o.Vragenlijst.Id,
                   Beloning = o.Beloning,
                   Status = o.Status,
                   Bedrijf = o.Bedrijf.Bedrijfsnaam,
                   Datum = o.Datum,
                   Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
                   {
                       Id = e.Id,
                       Email = e.Email,
                   }).ToList(),
                   Beperkingen = o.Beperkingen.Select(b => new BeperkingDto
                   {
                       Id = b.Id,
                       Naam = b.Naam
                   }).ToList(),
                   TypeOnderzoek = o.TypeOnderzoek.Naam
               })
               .FirstAsync();

            return Ok(onderzoek);
        }

        [HttpGet("medewerker")]
        public async Task<IActionResult> GetOnderzoekenStatus()
        {
            var onderzoeken = new ArrayList();
            var onderzoekenGoedgekeurd = await _context.Onderzoeken
                .Include(o => o.Bedrijf)
                .Include(o => o.TypeOnderzoek)
				.Where(o => o.Status != "In afwachting")
                .Select(o => new OnderzoekDto
                {
                    Id = o.Id,
                    Titel = o.Titel,
                    Omschrijving = o.Omschrijving,
                    Vragenlijst = o.Vragenlijst.Id,
                    Beloning = o.Beloning,
                    Status = o.Status,
                    Bedrijf = o.Bedrijf.Bedrijfsnaam,
                    Datum = o.Datum,
                    Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
                    {
                        Id = e.Id,
                        Email = e.Email,
                    }).ToList(),
                    Beperkingen = o.Beperkingen.Select(b => new BeperkingDto
                    {
                        Id = b.Id,
                        Naam = b.Naam
                    }).ToList(),
                    TypeOnderzoek = o.TypeOnderzoek.Naam
                })
                .ToListAsync();

            var onderzoekenAnderen = await _context.Onderzoeken
                .Include(o => o.Bedrijf)
                .Include(o => o.TypeOnderzoek)
                .Where(o => o.Status == "In afwachting")
                .Select(o => new OnderzoekDto
                {
                    Id = o.Id,
                    Titel = o.Titel,
                    Omschrijving = o.Omschrijving,
                    Vragenlijst = o.Vragenlijst.Id,
                    Beloning = o.Beloning,
                    Status = o.Status,
                    Bedrijf = o.Bedrijf.Bedrijfsnaam,
                    Datum = o.Datum,
                    Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
                    {
                        Id = e.Id,
                        Email = e.Email,
                    }).ToList(),
                    Beperkingen = o.Beperkingen.Select(b => new BeperkingDto
                    {
                        Id = b.Id,
                        Naam = b.Naam
                    }).ToList(),
                    TypeOnderzoek = o.TypeOnderzoek.Naam
                })
                .ToListAsync();

            var response = new OnderzoekenResponse
            {
                onderzoekenEerste = onderzoekenGoedgekeurd,
                onderzoekenTweede = onderzoekenAnderen
            };


            return Ok(response);
        }


        [Authorize(Roles = "Bedrijf")]
		// GET: api/<OnderzoekController>/Bedrijf
		[HttpGet("Bedrijf")]
		public async Task<IActionResult> GetOnderzoekBedrijf()
		{
			var bedrijfId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var bedrijf = await _context.Bedrijven
			  .Include(b => b.BedrijfsOnderzoekslijst)
			  .FirstOrDefaultAsync(b => b.Id == int.Parse(bedrijfId));

			if (bedrijf != null)    
			{
				var onderzoeks = await _context.Onderzoeken
				.Include(o => o.Bedrijf )
				.Include(o => o.TypeOnderzoek)
				.Where(o => o.Bedrijf.Id == int.Parse(bedrijfId))
				.Select(o => new OnderzoekDto
				{
					Id = o.Id,
					Titel = o.Titel,
					Omschrijving = o.Omschrijving,
					Vragenlijst = o.Vragenlijst.Id,
					Beloning = o.Beloning,
					Status = o.Status,
					Bedrijf = o.Bedrijf.Bedrijfsnaam,
					Datum = o.Datum,
					Ervaringsdeskundigen = o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
					{
						Id = e.Id,
						Email = e.Email,
					}).ToList(),
					Beperkingen = o.Beperkingen.Select(b => new BeperkingDto
					{
						Id = b.Id,
						Naam = b.Naam
					}).ToList(),
					TypeOnderzoek = o.TypeOnderzoek.Naam
				})
				.ToListAsync();
				return Ok(onderzoeks);
			}
			else{ throw new Exception("Bedrijf niet gevonden"); }

		}

		
		
		/*datum form:yyyy-MM-ddTHH:mm:ss*/
		/*voorbeeld onderzoek

		{
		   "titel":"Hallo",
		   "omschrijving":"het is omschrijving",
		   "Beloning":"Me",
		   "Status":"start",
		   "bedrijfId":3,
		   "Datum":"2024-12-01", 
		   "typeOnderzoek":"Fysiek",
			"beperkingen":[
			{
				"Id":1,
				"Naam":"Visueel"
			},
			{
				"Id":3,
				"Naam":"Motorisch"
			}
		]
		}

		 */

		// POST api/<OnderzoekController>
		[HttpPost("bedrijf/add")]
		public async Task<IActionResult> AddOnderzoeken([FromBody] OnderzoekForm model)
		{
			try
			{
				var bedrijfId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var beperkingen = _context.Beperkingen.Where(a => model.Beperkingen.Select(aa => aa.Id).Contains(a.Id)).ToList();
				var typeOnderzoek = await _context.TypeOnderzoeken.FirstOrDefaultAsync(t=>t.Naam == model.TypeOnderzoek);
				var bedrijf = await _context.Bedrijven.FirstOrDefaultAsync(b=>b.Id == int.Parse(bedrijfId));
		 
			   
				Onderzoek onderzoek = new()
			{
				Titel = model.Titel,
				Omschrijving = model.Omschrijving,
				Beloning = model.Beloning,
				Status = "In afwachting",
				Bedrijf =bedrijf,
				Datum = (DateTime)model.Datum,
				TypeOnderzoek = typeOnderzoek,
				Beperkingen = beperkingen
				};
			await _context.Onderzoeken.AddAsync(onderzoek);
			await _context.SaveChangesAsync();
			return Ok(new Response { Status = "Success", Message = "Onderzoek goed gemaakt " });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}



		[HttpGet("Beperkingen")]
		public async Task<IActionResult> GetBeperkingen()
		{
			var beperkingen = await _context.Beperkingen.ToListAsync();

			return Ok(beperkingen);
		}

        [HttpPut("ervaringdiskundigen/{id}")]
        public async Task<IActionResult> AddErvaringdeskundige(int id)
        {
            var onderzoek = await _context.Onderzoeken
                .Include(o => o.Ervaringsdeskundigen)
                .Where(o => o.Id == id)
                .SingleOrDefaultAsync(); ;
            var ervaringsdeskundigenModel = await _context.Onderzoeken
             .Where(o => o.Id == id)
             .Include(o => o.Ervaringsdeskundigen)
             .Select(o => o.Ervaringsdeskundigen.Select(e => new deskundigeEmailDto
             {
                 Id = e.Id,
                 Email = e.Email,
             }).ToList())
             .FirstOrDefaultAsync();
            var ervaringsdekundigen = _context.Ervaringsdeskundigen.Where(a => ervaringsdeskundigenModel.Select(aa => aa.Id).Contains(a.Id)).ToList();
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var deskundige = await _context.Ervaringsdeskundigen.FindAsync(userId);
            if (ervaringsdeskundigenModel.Any(e => e.Id == userId) && ervaringsdeskundigenModel != null)
            {

                onderzoek.Ervaringsdeskundigen.Clear();
                ervaringsdekundigen.Remove(deskundige);
               
            }
            else
            {
                ervaringsdekundigen.Add(deskundige);
            }
            onderzoek.Ervaringsdeskundigen = ervaringsdekundigen;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent(); 
        }





        [HttpPut("AkkordStatus/{id}")]
        public async Task<IActionResult> AkkordStatus(int id)
        {
            var onderzoek = await _context.Onderzoeken.FindAsync(id);
            onderzoek.Status = "Actief";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent(); 
        }

        [HttpPut("NietAkkordStatus/{id}")]
        public async Task<IActionResult> NietAkkordStatus(int id)
        {
            var onderzoek = await _context.Onderzoeken.FindAsync(id);
            onderzoek.Status = "Afgekeurd";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent(); 
        }

        [HttpGet("ervaringsdeskundigen")]
        public async Task<IActionResult> getErvaringsdeskundgien() 
        {
            var ervaringsdeskundigen = await _context.Ervaringsdeskundigen.ToListAsync();
            var erDto = ervaringsdeskundigen.Select(e => new deskundigeEmailDto
            {
                Id = e.Id,
                Email = e.Email,
            }).ToList();
            
            return Ok(erDto);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> updateOnderzoek([FromBody] OnderzoekForm onderzoekUpdates, int id)
        {
            var onderzoek = await _context.Onderzoeken
                .Include(o => o.Beperkingen)
                .Include(o=>o.Ervaringsdeskundigen)
                .Where(o=>o.Id == id)
                .SingleOrDefaultAsync();
            var beperkingen = _context.Beperkingen.Where(a => onderzoekUpdates.Beperkingen.Select(aa => aa.Id).Contains(a.Id)).ToList();
            var deelnemers = _context.Ervaringsdeskundigen.Where(a => onderzoekUpdates.Deelnemers.Select(aa => aa.Id).Contains(a.Id)).ToList();
            var typeOnderzoek = await _context.TypeOnderzoeken.FirstOrDefaultAsync(t => t.Naam == onderzoekUpdates.TypeOnderzoek);
            // Controleer of de email is gewijzigd

           onderzoek.Titel = onderzoekUpdates.Titel;
           onderzoek.Omschrijving = onderzoekUpdates.Omschrijving;
           onderzoek.Beloning = onderzoekUpdates.Beloning;
           onderzoek.Datum = (DateTime)onderzoekUpdates.Datum;
           onderzoek.TypeOnderzoek = typeOnderzoek;
           onderzoek.Beperkingen.Clear();
           onderzoek.Beperkingen = beperkingen;
           onderzoek.Ervaringsdeskundigen.Clear();
           onderzoek.Ervaringsdeskundigen = deelnemers;


            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // Geef bijgewerkte gegevens terug als succesvol
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Er is een fout opgetreden bij het bijwerken van het bedrijf.");
            }
        }


        // DELETE api/<OnderzoekController>/5
        [HttpDelete("verwijderen/{id}")]
        public async Task<IActionResult> VerwijderOnderzoek(int id)
		{
			try {
                var onderzoek = await _context.Onderzoeken.FindAsync(id);
                 _context.Onderzoeken.Remove(onderzoek);
                await _context.SaveChangesAsync();
            }
			catch
			{
				throw new Exception("niet gelukkig");
			}
            return Ok(id);
        }

    }
}

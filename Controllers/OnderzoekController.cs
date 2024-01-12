﻿using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Versioning;
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

        [Authorize(Roles = "Bedrijf")]
        // GET: api/<OnderzoekController>/Bedrijf
        [HttpGet("Bedrijf")]
        public async Task<IActionResult> GetOnderzoekBedrijf()
        {
            var bedrijfId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var bedrijf = await _context.Bedrijven
              .Include(b => b.BedrijfsOnderzoekslijst)
              .FirstOrDefaultAsync(b => b.Id == bedrijfId);

            if (bedrijf != null)    
            {
                var onderzoeks = await _context.Onderzoeken
                .Include(o => o.Bedrijf )
                .Include(o => o.TypeOnderzoek)
                .Where(o => o.Bedrijf.Id == bedrijfId)
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

        // GET api/<OnderzoekController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        [HttpPost]
        public async Task<IActionResult> Onderzoeken([FromBody] OnderzoekForm model)
        {
            try
            {
                var beperkingen = _context.Beperkingen.Where(a => model.Beperkingen.Select(aa => aa.Id).Contains(a.Id)).ToList();
                var typeOnderzoek = await _context.TypeOnderzoeken.FirstOrDefaultAsync(t=>t.Naam == model.TypeOnderzoek);
            var bedrijf = await _context.Bedrijven.FirstOrDefaultAsync(b=>b.Id == model.BedrijfId);
         
               
                Onderzoek onderzoek = new()
            {
                Titel = model.Titel,
                Omschrijving = model.Omschrijving,
                Beloning = model.Beloning,
                Status = model.Status,
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

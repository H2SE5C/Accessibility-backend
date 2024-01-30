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

namespace Accessibility_app.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VraagController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public VraagController(ApplicationDbContext applicationDbContext) {
            _context = applicationDbContext;
        }

        // GET: api/<VraagController>/get-all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllVragen()
        {
            var vragen = await _context.Vragen
                .Select(v => new VraagDto
                {
                    Id = v.Id,
                    VraagTekst = v.VraagTekst,
                    VragenlijstId = v.VragenlijstId
                })
                .ToListAsync();

            return Ok(vragen);
        }
    }
}

﻿using Accessibility_app.Models;
using Accessibility_backend;
using Accessibility_backend.Modellen.Registreermodellen;
using Accessibility_backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

[Route("api/[controller]")]
[ApiController]
public class AuthenticatieController : ControllerBase
{
    private readonly IAuthenticatieService _authenticatieService;
    private readonly IEmailSender _emailSender;
    public AuthenticatieController(
        IAuthenticatieService authenticatieService,
        IEmailSender emailSender)
    {
        _authenticatieService = authenticatieService;
        _emailSender = emailSender;
    }

    //dit kan ergens anders zodat bedrijf het ook kan gebruiken misschien? idk
    [HttpGet("/verifieer")]
    public async Task<IActionResult> VerifieerEmail(string token, string email)
    {
        var result = await _authenticatieService.VerifieerEmail(token, email);
        return result;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var result = await _authenticatieService.Login(model);

        return result;
        /* }
         return Unauthorized();*/
    }

    [HttpPost]
    [Route("/loguit")]
    public IActionResult LogUit()
    {
        var result = _authenticatieService.LogUit();
        return result;
    }

    [HttpGet]
    [Route("/refresh")]
    public async Task<IActionResult> Refresh()
    {
        var result = await _authenticatieService.Refresh();
        return result;
    }

    //registratie vereist een goede wachtwoord: 1 hoofdletter, cijfer en rare teken
    [HttpPost]

    [HttpPost("registreer-beheerder")]
    public async Task<IActionResult> RegistreerBeheerder([FromBody] RegistrerenBasis model)
    {
        var result = await _authenticatieService.RegistreerBeheerder(model);
        return result;
    }

    [HttpPost("registreer-developer")]
    public async Task<IActionResult> RegistreerDeveloper([FromBody] RegistrerenBasis model)
    {
        var result = await _authenticatieService.RegistreerDeveloper(model);
        return result;
    }

    [HttpPost("registreer-medewerker")]
    public async Task<IActionResult> RegistreerMedewerker([FromBody] RegistrerenMedewerkers model)
    {
        var result = await _authenticatieService.RegistreerMedewerker(model);
        return result;
    }


    // POST api/Authenticatie/registreer-Bedrijf
    //voorbeeld:
    /*{
        "Bedrijfsnaam": "AH",
        "Omschrijving": "Awesome",
        "wachtwoord": "String123@",
        "email": "Killer@example.com",
        "Locatie": "Amsterdam 21",
        "LinkNaarBedrijf": "https://www.ah.nl/?gad_source=1&gclid=CjwKCAiA7t6sBhAiEiwAsaieYoHEM-7ASRzYNpFhpGe3o3Q7bwtwR4_96ouq7Q_cLXJEli9IenU3EhoCB_MQAvD_BwE"

        }*/
    [HttpPost("registreer-bedrijf")]
    public async Task<IActionResult> RegistreerBedrijf([FromBody] RegistrerenBedrijf model)
    {
        var result = await _authenticatieService.RegistreerBedrijf(model);
        return result;
    }

    // post /api/Authenticatie/registreer-ervaringsdeskundige
    //voorbeeld:
    /*{
        "voornaam": "Dude",
        "achternaam": "Awesome",
        "wachtwoord": "String123@",
        "email": "Killer@example.com",
        "postcode": "2224GE",
        "minderjarig": false,
        "telefoonnummer": "0684406262",
        "aandoeningen" : [
        {
              "id": 2,
              "naam": "Slechtziendheid"
        },
        {
              "id": 4,
             "naam": "ADHD"
        }
        ],
        "typeOnderzoeken": [
        {
            "id": 1,
            "naam": "Vragenlijst"
        }
        ],
        "voorkeurBenadering": "geen voorkeur",
        "commerciële": false
        }*/
    [HttpPost("registreer-ervaringsdeskundige")]
    public async Task<IActionResult> RegistreerErvaringsdeskundige([FromBody] RegistrerenErvaringdeskundige model)
    {
        var result = await _authenticatieService.RegistreerErvaringsdeskundige(model);
        if (result is OkObjectResult okObjectResult)
        {
            //het geeft email daarna token
            dynamic data = okObjectResult.Value;
            var emailResult = await VerificatieEmailVerzenden(data.Email, data.Token);
            return emailResult;
        }
        return result;
    }

    [HttpDelete("{id}")]
    //string id? niet int?
    public async Task<IActionResult> VerwijderGebruiker(string id)
    {
        var result = await _authenticatieService.VerwijderGebruiker(id);
        return result;
    }

    private async Task<IActionResult> VerificatieEmailVerzenden(string email, string token)
    {
        var link = Url.Action(nameof(VerifieerEmail), "Authenticatie", new { token, email }, Request.Scheme);
        var result = await _emailSender.SendEmailAsync(email, "Verifieer email - Accessibility", link);

        if (result.Status.Equals("Success"))
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
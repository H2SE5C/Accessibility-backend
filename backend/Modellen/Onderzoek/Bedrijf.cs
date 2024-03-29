﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Bedrijf")]
	public class Bedrijf : Gebruiker
    {
        public string Bedrijfsnaam { get; set; }
        public string Omschrijving { get; set; }
        public string Locatie { get; set; }
        public string LinkNaarBedrijf { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<Onderzoek> BedrijfsOnderzoekslijst { get; set; } = new();

    }
}
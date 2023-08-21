﻿using System;

namespace RegistrationClinik.Models
{
    public class ShowTableModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birday { get; set; }
        public string? Adres { get; set; }
        public string? Analiz { get; set; }
        public string? LDoctor { get; set; }
        public string? TelNumber { get; set; }
        public string? PalataNumber { get; set; }
        public DateTime? RegistrationDate { get; set; } = DateTime.UtcNow.Date;
        public decimal? Bonus { get; set; } = 0;
        public decimal? KajBro { get; set; } = 0;
        public decimal? Oplacheno { get; set; } = 0;
        public decimal? Oplata { get; set; } = 0;
        public string? Comments { get; set; }
        public int IsShow { get; set; } = 0;
        public int? Number { get; set; }
        public string? BackColor { get; set; } = "Blue";
    }
}

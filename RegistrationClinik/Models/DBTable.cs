using System;

namespace RegistrationClinik.Models
{
    public class DBTable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birday { get; set; }
        public string? Adres { get; set; }
        public string? Analiz { get; set; }
        public string? LDoctor { get; set; }
        public string? TelNumber { get; set; }
        public string? PalataNumber { get; set; }
        public DateTime? RegistrationDate { get; set; } = DateTime.UtcNow;
        public decimal? Bonus { get; set; } = 0;
        public string? KajBro { get; set; }
        public decimal? Ostatok { get; set; } = 0;
        public decimal? Oplata { get; set; } = 0;
        public string? Comments { get; set; }
        public int IsShow { get; set; } = 0;
    }
}

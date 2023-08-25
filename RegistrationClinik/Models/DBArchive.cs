using System;

namespace RegistrationClinik.Models
{
    public class DBArchive
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birday { get; set; }
        public string? Adres { get; set; }
        public string? Analiz { get; set; }
        public string? TelNumber { get; set; }
        public string? LDoctor { get; set; }
        public string? PalataNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public decimal? Oplata { get; set; }
        public int IsShow { get; set; } = 1;
    }
}

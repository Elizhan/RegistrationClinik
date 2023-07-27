using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime? RegistrationDate { get; set; } = DateTime.UtcNow.Date;
        public decimal? Avans { get; set; } = 0;
        public decimal? Ostatok { get; set; } = 0;
        public decimal? Oplacheno { get; set; } = 0;
        public decimal? Oplata { get; set; } = 0;
        public int IsShow { get; set; } = 1;
        public int? Number { get; set; }
    }
}

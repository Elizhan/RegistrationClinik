using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationClinik.Models
{
    public class DBTableB
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birday { get; set; }
        public string? Adres { get; set; }
        public string? Analiz { get; set; }
        public string? LDoctor { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public decimal? Avans { get; set; }
        public decimal? Ostatok { get; set; }
        public decimal? Oplacheno { get; set; }
        public decimal? Oplata { get; set; }
    }
}

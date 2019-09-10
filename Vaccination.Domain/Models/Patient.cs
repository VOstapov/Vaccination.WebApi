using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Soname { get; set; }
        public string Name { get; set; }
        public string Patronomic { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Snils { get; set; }
    }
}

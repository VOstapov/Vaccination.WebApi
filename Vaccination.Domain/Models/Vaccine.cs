using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.Models
{
    public class Vaccine
    {
        public int Id { get; set; }

        public string Medication { get; set; }

        public bool Agreement { get; set; }

        public DateTime Date { get; set; }

        public int PatientId { get; set; }
    }
}

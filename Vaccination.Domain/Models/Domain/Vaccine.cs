using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.Models.Domain
{
    public class Vaccine : BaseModel
    {
        public Medication Medication { get; set; }

        public int MedicationId { get; set; }

        public bool Agreement { get; set; }

        public DateTime Date { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}

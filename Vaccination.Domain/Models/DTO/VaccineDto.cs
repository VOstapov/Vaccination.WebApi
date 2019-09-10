using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.Models.DTO
{
    public class VaccineDto : BaseModel
    {
        public string Medication { get; set; }

        public bool Agreement { get; set; }

        public DateTime Date { get; set; }

        public int PatientId { get; set; }
    }
}

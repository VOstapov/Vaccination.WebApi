using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.Models.DTO
{
    public class PatientDto : BaseModel
    {
        public string Soname { get; set; }
        public string Name { get; set; }
        public string Patronomic { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Snils { get; set; }
    }
}

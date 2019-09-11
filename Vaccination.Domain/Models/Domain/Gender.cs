using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.Models.Domain
{
    public class Gender : BaseModel
    {
        public string Name { get; set; }
        public virtual IEnumerable<Patient> Patients {get; set;}
    }
}

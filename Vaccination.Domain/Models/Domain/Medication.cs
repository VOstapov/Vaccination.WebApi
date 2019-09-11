using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.Models.Domain
{
    public class Medication : BaseModel
    {
        public string Name { get; set; }

        public virtual IEnumerable<Vaccine> Vaccines { get; set; }
    }
}

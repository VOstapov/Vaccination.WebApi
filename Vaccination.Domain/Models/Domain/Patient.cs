using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.Models.Domain
{
    public class Patient : BaseModel
    {
        public string Soname { get; set; }
        public string Name { get; set; }
        public string Patronomic { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public int GenderId { get; set; }
        public string Snils { get; set; }

        public virtual IEnumerable<Vaccine> Vaccines { get; set; }
    }
}

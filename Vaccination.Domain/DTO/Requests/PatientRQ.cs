﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccination.Domain.DTO.Requests
{
    public class PatientRQ
    {
        public string Soname { get; set; }
        public string Name { get; set; }
        public string Patronomic { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Snils { get; set; }
    }
}

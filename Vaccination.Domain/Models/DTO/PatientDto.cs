using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vaccination.Common.Attributes;

namespace Vaccination.Domain.Models.DTO
{
    public class PatientDto : BaseModel
    {
        [Required]
        [StringLength(200)]
        public string Soname { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Patronomic { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidation]
        public DateTime Birthday { get; set; }

        [Required]
        [GenderValidation]
        public string Gender { get; set; }

        [Required]
        [SnilsValidation]
        public string Snils { get; set; }
    }
}

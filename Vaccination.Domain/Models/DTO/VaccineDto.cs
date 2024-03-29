﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vaccination.Common.Attributes;

namespace Vaccination.Domain.Models.DTO
{
    public class VaccineDto : BaseModel
    {
        [Required]
        public MedicationDto Medication { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Agreement required true.")]
        public bool Agreement { get; set; }

        [Required]
        [DateValidation]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PatientId { get; set; }
    }
}

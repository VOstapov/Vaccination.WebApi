using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Vaccination.Domain.Models.Domain;
using Vaccination.Domain.Models.DTO;

namespace Vaccination.Domain.Mapping
{
    public class MedicationMapping : Profile
    {
        public MedicationMapping()
        {
            CreateMap<Medication, MedicationDto>();
            CreateMap<MedicationDto, Medication>();
        }
    }
}

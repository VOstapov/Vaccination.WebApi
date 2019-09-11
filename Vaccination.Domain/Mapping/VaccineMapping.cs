using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Vaccination.Domain.Models.Domain;
using Vaccination.Domain.Models.DTO;

namespace Vaccination.Domain.Mapping
{
    public class VaccineMapping : Profile
    {
        public VaccineMapping()
        {
            CreateMap<Vaccine, VaccineDto>();
            CreateMap<VaccineDto, Vaccine>()
                .ForMember(dest => dest.Medication,
                    opt => opt.Ignore());
        }
    }
}

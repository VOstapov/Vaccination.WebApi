using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Vaccination.Domain.Models.Domain;
using Vaccination.Domain.Models.DTO;

namespace Vaccination.Domain.Mapping
{
    public class GenderMapping : Profile
    {
        public GenderMapping()
        {
            CreateMap<Gender, GenderDto>();
            CreateMap<GenderDto, Gender>();
        }
    }
}

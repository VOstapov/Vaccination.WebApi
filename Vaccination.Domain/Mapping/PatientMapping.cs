using AutoMapper;
using Vaccination.Domain.Models.Domain;
using Vaccination.Domain.Models.DTO;

namespace Vaccination.Domain.Mapping
{
    public class PatientMapping : Profile
    {
        public PatientMapping()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>()
                .ForMember(dest => dest.Gender,
                    opt => opt.Ignore());
        }
    }
}

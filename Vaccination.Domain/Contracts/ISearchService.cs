using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vaccination.Domain.Models.DTO;

namespace Vaccination.Domain.Contracts
{
    public interface ISearchService
    {
        Task<IEnumerable<PatientDto>> FindPatientsAsync(string searchString = "");
    }
}

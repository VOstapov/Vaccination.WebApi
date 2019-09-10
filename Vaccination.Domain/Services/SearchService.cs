using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.Domain.Contracts;
using Vaccination.Domain.Models.Domain;
using Vaccination.Domain.Models.DTO;

namespace Vaccination.Domain.Services
{
    public class SearchService : ISearchService
    {
        private readonly IService<PatientDto> service;

        public SearchService(IService<PatientDto> service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IEnumerable<PatientDto>> FindPatientsAsync(string searchString = "")
        {
            if (string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(searchString.Trim()))
            {
                return await service.GetAllAsync().ConfigureAwait(false);
            }

            var splitted = searchString.Trim().Split(" ");

            // На больших объемах будет менее эффективно..
            return await service
                .GetAllAsync(x => splitted
                    .Any(a => x.Patronomic.Contains(a, StringComparison.InvariantCultureIgnoreCase)
                        || x.Name.Contains(a, StringComparison.InvariantCultureIgnoreCase)
                        || x.Soname.Contains(a, StringComparison.InvariantCultureIgnoreCase)
                        || x.Snils.Contains(a, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}

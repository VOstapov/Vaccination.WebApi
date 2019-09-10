using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.Domain.Contracts;
using Vaccination.Domain.DTO.Requests;
using Vaccination.Domain.DTO.Responses;
using Vaccination.Domain.Models;

namespace Vaccination.Domain.Services
{
    public class GenericService<TRequest, TDomain, TResponse> : IService<TRequest, TDomain, TResponse>
    {
        private readonly IRepository<TDomain> repository;

        public GenericService(IRepository<TDomain> repository)
        {
            this.repository = repository;
        }

        public Task<TResponse> AddAsync(TRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<TDomain, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TResponse>> GetAllAsync()
        {
            var domain = await this.repository.GetAllAsync().ConfigureAwait(false);

            throw new NotImplementedException();
        }

        public Task<TResponse> GetAsync(Expression<Func<TDomain, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> UpdateAsync(TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

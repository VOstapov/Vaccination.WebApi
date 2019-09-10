using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.Domain.DTO.Requests;
using Vaccination.Domain.DTO.Responses;
using Vaccination.Domain.Models;

namespace Vaccination.Domain.Contracts
{
    public interface IService<TRequest, TDomain, TResponse>
    {
        Task<IEnumerable<TResponse>> GetAllAsync();

        Task<TResponse> GetAsync(Expression<Func<TDomain, bool>> predicate);

        Task<TResponse> AddAsync(TRequest request);

        Task<TResponse> UpdateAsync(TRequest request);

        Task DeleteAsync(Expression<Func<TDomain, bool>> predicate);
    }
}

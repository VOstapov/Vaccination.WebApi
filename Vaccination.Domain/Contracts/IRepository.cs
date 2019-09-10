using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.Domain.Contracts
{
    public interface IRepository<TDomain>
    {
        Task<IEnumerable<TDomain>> GetAllAsync();

        Task<TDomain> GetAsync(Expression<Func<TDomain, bool>> predicate);

        Task DeleteAsync(Expression<Func<TDomain, bool>> predicate);

        Task<TDomain> AddAsync(TDomain domain);

        Task<TDomain> UpdateAsync(TDomain domain);
    }
}

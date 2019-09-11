using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.Domain.Contracts
{
    public interface IRepository<TDomain>
    {
        Task<IEnumerable<TDomain>> GetAllAsync(params Expression<Func<TDomain, object>>[] includeProperties);

        Task<IEnumerable<TDomain>> GetAllAsync(Expression<Func<TDomain, bool>> predicate, params Expression<Func<TDomain, object>>[] includeProperties);

        Task<TDomain> GetAsync(Expression<Func<TDomain, bool>> predicate, params Expression<Func<TDomain, object>>[] includeProperties);

        Task<TDomain> DeleteAsync(TDomain domain);

        Task<TDomain> AddAsync(TDomain domain);

        Task<TDomain> UpdateAsync(TDomain domain);
    }
}

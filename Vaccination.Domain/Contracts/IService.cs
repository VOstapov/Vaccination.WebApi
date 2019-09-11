using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.Domain.Models;

namespace Vaccination.Domain.Contracts
{
    public interface IService<TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<TDto, object>>[] includeProperties);

        Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TDto, bool>> predicate, params Expression<Func<TDto, object>>[] includeProperties);

        Task<TDto> GetAsync(Expression<Func<TDto, bool>> predicate, params Expression<Func<TDto, object>>[] includeProperties);

        Task<TDto> AddAsync(TDto dto);

        Task<TDto> UpdateAsync(TDto dto, Expression<Func<TDto, bool>> predicate);

        Task<TDto> DeleteAsync(Expression<Func<TDto, bool>> predicate);
    }
}

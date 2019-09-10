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
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TDto, bool>> predicate);

        Task<TDto> GetAsync(Expression<Func<TDto, bool>> predicate);

        Task<TDto> AddAsync(TDto dto);

        Task<TDto> UpdateAsync(TDto dto, Expression<Func<TDto, bool>> predicate);

        Task<TDto> DeleteAsync(Expression<Func<TDto, bool>> predicate);
    }
}

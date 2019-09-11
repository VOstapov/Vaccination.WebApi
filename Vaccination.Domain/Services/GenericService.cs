using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.Domain.Contracts;
using Vaccination.Domain.Models;

namespace Vaccination.Domain.Services
{
    public class GenericService<TDomain, TDto> : IService<TDto>
        where TDomain : BaseModel
        where TDto : BaseModel
    {
        private readonly IRepository<TDomain> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GenericService(IRepository<TDomain> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            var domain = mapper.Map<TDomain>(dto);
            domain = await repository.AddAsync(domain).ConfigureAwait(false);
            await unitOfWork.SaveAsync().ConfigureAwait(false);
            return mapper.Map<TDto>(domain);
        }

        public async Task<TDto> DeleteAsync(Expression<Func<TDto, bool>> predicate)
        {
            var expr = mapper.Map<Expression<Func<TDomain, bool>>>(predicate);
            var domain = await repository.GetAsync(expr);
            if (domain != null)
            {
                domain = await repository.DeleteAsync(domain).ConfigureAwait(false);
                await unitOfWork.SaveAsync().ConfigureAwait(false);
            }

            return mapper.Map<TDto>(domain);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<TDto, object>>[] includeProperties)
        {
            var domain = await repository.GetAllAsync(MapIncludeProperties(includeProperties)).ConfigureAwait(false);
            return mapper.Map<IEnumerable<TDto>>(domain);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync(
            Expression<Func<TDto, bool>> predicate,
            params Expression<Func<TDto, object>>[] includeProperties)
        {
            var expr = mapper.Map<Expression<Func<TDomain, bool>>>(predicate);
            var domain = await repository.GetAllAsync(expr, MapIncludeProperties(includeProperties)).ConfigureAwait(false);
            return mapper.Map<IEnumerable<TDto>>(domain);
        }

        public async Task<TDto> GetAsync(
            Expression<Func<TDto, bool>> predicate,
            params Expression<Func<TDto, object>>[] includeProperties)
        {
            var expr = mapper.Map<Expression<Func<TDomain, bool>>>(predicate);
            var domain = await repository.GetAsync(expr, MapIncludeProperties(includeProperties)).ConfigureAwait(false);

            return mapper.Map<TDto>(domain);
        }

        public async Task<TDto> UpdateAsync(TDto dto, Expression<Func<TDto, bool>> predicate)
        {
            var expr = mapper.Map<Expression<Func<TDomain, bool>>>(predicate);
            var domain = await repository.GetAsync(expr).ConfigureAwait(false);

            if (domain != null)
            {
                domain = mapper.Map<TDto, TDomain>(dto, domain);
                domain = await repository.UpdateAsync(domain).ConfigureAwait(false);
                await unitOfWork.SaveAsync().ConfigureAwait(false);
            }
            return mapper.Map<TDto>(domain);
        }

        private Expression<Func<TDomain, object>>[] MapIncludeProperties(
            params Expression<Func<TDto, object>>[] includeProperties)
        {
            Expression<Func<TDomain, object>>[] targetProperties = Enumerable.Empty<Expression<Func<TDomain, object>>>().ToArray();
            if (includeProperties.Any())
            {
                targetProperties = includeProperties.Select(x => mapper.Map<Expression<Func<TDomain, object>>>(x)).ToArray();
            }

            return targetProperties;
        }
    }
}

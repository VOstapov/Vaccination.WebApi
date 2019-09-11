using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.Domain.Contracts;

namespace Vaccination.DataAccess.Repository
{
    public class GenericRepository<TDomain> : IRepository<TDomain> where TDomain : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TDomain> dbSet;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = dbContext.Set<TDomain>();
        }

        public async Task<TDomain> AddAsync(TDomain domain)
        {
            var r = await dbSet.AddAsync(domain).ConfigureAwait(false);
            return r.Entity;
        }

        public Task<TDomain> DeleteAsync(TDomain domain)
        {
            dbSet.Remove(domain);
            return Task.FromResult(domain);
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync(params Expression<Func<TDomain, object>>[] includeProperties)
        {
            return await includeProperties
                .Aggregate(dbSet.AsNoTracking(), (current, includeProperty) => current.Include(includeProperty))
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync(
            Expression<Func<TDomain, bool>> predicate,
            params Expression<Func<TDomain, object>>[] includeProperties)
        {
            return await includeProperties
                .Aggregate(dbSet.AsNoTracking(), (current, includeProperty) => current.Include(includeProperty))
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<TDomain> GetAsync(
            Expression<Func<TDomain, bool>> predicate,
            params Expression<Func<TDomain, object>>[] includeProperties)
        {
            return await includeProperties
                .Aggregate(dbSet.AsNoTracking(), (current, includeProperty) => current.Include(includeProperty))
                .FirstOrDefaultAsync(predicate)
                .ConfigureAwait(false);
        }

        public Task<TDomain> UpdateAsync(TDomain domain)
        {
            dbContext.Entry(domain).State = EntityState.Modified;
            return Task.FromResult(domain);
        }
    }
}

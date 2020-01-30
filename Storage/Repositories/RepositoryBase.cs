using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using RatesParsingWeb.Extentions;

namespace RatesParsingWeb.Storage.Repositories
{
    /// <summary>
    /// Базовый класс для работы с сущностями БД.
    /// </summary>
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly BankRatesContext bankRatesContext;
        protected DbSet<T> dbSet;

        public RepositoryBase(BankRatesContext context)
        {
            bankRatesContext = context;
            dbSet = bankRatesContext.Set<T>();
        }

        public virtual async Task AddAsync(T entity) =>
            await bankRatesContext.AddAsync(entity);

        public virtual Task AddRangeAsync(T[] entity) =>
            bankRatesContext.AddRangeAsync(entity);

        public virtual async Task<T> GetByIdAsync(int id) =>
            await dbSet.FindAsync(id);

        public virtual Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where) =>
            dbSet.FirstOrDefaultAsync(where);

        public virtual async Task<IEnumerable<T>> GetAllAsync() =>
            await dbSet.ToArrayAsync();

        public virtual async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes) =>
             await dbSet.IncludeEntities(includes).ToArrayAsync();

        public virtual async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where) =>
            await dbSet.Where(where).ToArrayAsync();

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> where) =>
            dbSet.AnyAsync(where);

        public virtual Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
                return dbSet.CountAsync();
            else
                return dbSet.CountAsync(where);
        }

        public IQueryable<T> Query() =>
            dbSet.AsQueryable();

        public virtual async Task SaveChangesAsync() =>
            await bankRatesContext.SaveChangesAsync();

        public void SetStateModifed(T t) =>
            bankRatesContext.Attach(t).State = EntityState.Modified;

    }
}
// Гуглить Generic Repository, Unit of work.
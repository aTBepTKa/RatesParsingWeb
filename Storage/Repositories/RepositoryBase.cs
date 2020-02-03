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
        protected readonly DbSet<T> dbSet;

        public RepositoryBase(BankRatesContext context)
        {
            bankRatesContext = context;
            dbSet = bankRatesContext.Set<T>();
        }

        public virtual async Task AddAsync(T entity) =>
            await dbSet.AddAsync(entity);

        public virtual Task AddRangeAsync(T[] entity) =>
            dbSet.AddRangeAsync(entity);

        public virtual T Find(int id) =>
            dbSet.Find(id);

        public virtual async Task<T> FindAsync(int id) =>
            await dbSet.FindAsync(id);

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
                                                    params Expression<Func<T, object>>[] includes) =>
            await dbSet.GetIncludes(includes).SingleAsync(predicate);

        public virtual Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where,
                                                     params Expression<Func<T, object>>[] includes) =>
            dbSet.GetIncludes(includes).FirstOrDefaultAsync(where);

        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where,
                                                               params Expression<Func<T, object>>[] includes) =>
            await dbSet.GetIncludes(includes).Where(where).ToArrayAsync();

        public virtual async Task<IEnumerable<T>> GetAllAsync(
            params Expression<Func<T, object>>[] includes) =>
            await dbSet.GetIncludes(includes).ToArrayAsync();


        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
            await dbSet.AnyAsync(predicate);

        public virtual bool Any(Expression<Func<T, bool>> predicate) =>
            dbSet.Any(predicate);

        public Task<bool> AnyWhereAsync(Expression<Func<T, bool>> where, Expression<Func<T, bool>> predicate) =>
            dbSet.Where(where).AnyAsync(predicate);

        public virtual Task<int> CountAsync(Expression<Func<T, bool>> predicate = null) =>
             dbSet.CountAsync(predicate);

        public IQueryable<T> Query(params Expression<Func<T, object>>[] includes) =>
            dbSet.GetIncludes(includes).AsQueryable();

        public virtual async Task SaveChangesAsync() =>
            await bankRatesContext.SaveChangesAsync();

        public void SetStateModifed(T t) =>
            bankRatesContext.Attach(t).State = EntityState.Modified;
    }
}
// Гуглить Generic Repository, Unit of work.
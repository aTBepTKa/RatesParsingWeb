using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using RatesParsingWeb.Storage.Repositories.Interfaces;

namespace RatesParsingWeb.Storage.Repositories
{
    /// <summary>
    /// Базовый класс для работы с сущностями БД.
    /// </summary>
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly BankRatesContext bankRatesContext;
        protected DbSet<T> _dbSet;

        public RepositoryBase(BankRatesContext context)
        {
            bankRatesContext = context;
            _dbSet = bankRatesContext.Set<T>();
        }

        /// <summary>
        /// Добавить элемент.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(T entity) =>
            await bankRatesContext.AddAsync(entity);

        /// <summary>
        /// Добавить массив элементов.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task AddRangeAsync(T[] entity) =>        
            bankRatesContext.AddRangeAsync(entity);

        /// <summary>
        /// Получить элемент по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        /// <summary>
        /// Получить элементы согласно выражению.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where) =>
            _dbSet.FirstOrDefaultAsync(where);

        /// <summary>
        /// Получить последовательность элементов.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAll() =>
            await _dbSet.ToArrayAsync();

        /// <summary>
        /// Получить последовательность элементов согласно выражению.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where) =>
            await _dbSet.Where(where).ToArrayAsync();

        /// <summary>
        /// Определить существют ли элементы в последовательности, удовлетворяющие выражению.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> where) =>
            _dbSet.AnyAsync(where);

        /// <summary>
        /// Возвращает количество элементов в последовательности.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
                return _dbSet.CountAsync();
            else
                return _dbSet.CountAsync(where);
        }

        /// <summary>
        /// Получить последовательность элементов как IQueryable (медленнее но с наименьшими затратами ресурсов).
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Query() =>
            _dbSet.AsQueryable();

        /// <summary>
        /// Сохранить изменения в базе данных.
        /// </summary>
        /// <returns></returns>
        public virtual async Task SaveChangesAsync() =>
            await bankRatesContext.SaveChangesAsync();

        /// <summary>
        /// Установить модифицированное состояние объекта. 
        /// </summary>
        /// <param name="t"></param>
        public void SetStateModifed(T t) =>
            bankRatesContext.Attach(t).State = EntityState.Modified;

        // TODO: добавить OrderBy.
    }
}
// Гуглить Generic Repository, Unit of work.
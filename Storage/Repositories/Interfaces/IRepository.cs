using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(T[] entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        Task<int> CountAsync(Expression<Func<T, bool>> where = null);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> Query();
        Task SaveChangesAsync();
        void SetStateModifed(T t);
    }
}

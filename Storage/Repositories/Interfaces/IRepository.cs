using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Добавить элемент.
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Добавить массив элементов.
        /// </summary>
        Task AddRangeAsync(T[] entity);

        /// <summary>
        /// Определить существют ли элементы в последовательности, удовлетворяющие выражению.
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// Возвращает количество элементов в последовательности.
        /// </summary>
        Task<int> CountAsync(Expression<Func<T, bool>> where = null);

        /// <summary>
        /// Получить все элементы.
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Получить элемент по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(int id);

        /// <summary>
        /// Получить элемент по Id асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync(int id);

        /// <summary>
        /// Плдучить единственный элемент последовательности.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Получить элемент согласно выражению.
        /// </summary>
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Получить последовательность элементов согласно выражению.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Возвращает объект типа IQueryable<T>.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// Установить модифицированное состояние объекта. 
        /// </summary>
        /// <param name="t"></param>
        void SetStateModifed(T t);

    }
}

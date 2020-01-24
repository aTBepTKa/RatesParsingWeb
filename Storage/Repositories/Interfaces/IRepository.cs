﻿using System;
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
        /// Получить последовательность элементов.
        /// </summary>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Получить элемент по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Получить элементы согласно выражению.
        /// </summary>
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// Получить последовательность элементов согласно выражению.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where);

        /// <summary>
        /// Получить последовательность элементов как IQueryable (медленнее но с наименьшими затратами ресурсов).
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query();

        /// <summary>
        /// Сохранить изменения в базе данных.
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

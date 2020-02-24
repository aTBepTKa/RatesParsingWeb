using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Extentions
{
    /// <summary>
    /// Содержит методы расширения для репозиториев.
    /// </summary>
    public static class RepositoryExtention
    {
        /// <summary>
        /// Получить последовательность объектов, включая зависимые объекты.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static IQueryable<T> GetIncludes<T>(this IQueryable<T> query,
            params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}

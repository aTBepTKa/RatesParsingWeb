using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Extentions
{
    /// <summary>
    /// Методы расширения для выражений.
    /// </summary>
    public static class ExpressionExtention
    {
        /// <summary>
        /// Добавить новое выражение к текущему - НЕ РАБОТАЕТ С EF.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right">Добавляемое выражение.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CombineWith<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var param = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(
                Expression.Invoke(left, param),
                Expression.Invoke(right, param));
            var lamdba = Expression.Lambda<Func<T, bool>>(body, param);            
            return lamdba;
        }
    }
}

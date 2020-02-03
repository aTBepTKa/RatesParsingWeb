using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        /// <summary>
        /// Получить все элементы.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        IValidation Validation { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Dto;
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

        /// <summary>
        /// Получить элемент по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Содержит данные о валидации.
        /// </summary>
        IValidationService ValidationDictionary { get; }
    }
}

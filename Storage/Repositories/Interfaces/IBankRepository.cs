using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories.Interfaces
{
    public interface IBankRepository : IRepository<Bank>
    {
        /// <summary>
        /// Возвращает банк по ID с основной валютой банка.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        new Task<Bank> GetByIdAsync(int id);

        /// <summary>
        /// Получить банк по ID включая зависимые объекты.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<Bank> GetByIdAsync(int id, params Expression<Func<Bank, object>>[] expression);

        /// <summary>
        /// Получить список банков с основной валютой.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Bank>> GetAllWithCurrenciesAsync();

        /// <summary>
        /// Возвращает банк по ID с основной валютой банка и настройками парсинга.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Bank> GetByIdWithSettingsAcync(int id);
    }
}

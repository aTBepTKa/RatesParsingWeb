using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Получить список банков с основной валютой.
        /// </summary>
        /// <returns></returns>
        new Task<IEnumerable<Bank>> GetAll();

        /// <summary>
        /// Возвращает банк по ID с основной валютой банка и настройками парсинга.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Bank> GetByIdWithSettingsAcync(int id);
    }
}

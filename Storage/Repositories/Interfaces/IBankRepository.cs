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
        /// Возвращает банк по ID с основной валютой банка и настройками парсинга.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Bank> GetByIdWithSettings(int id);
    }
}

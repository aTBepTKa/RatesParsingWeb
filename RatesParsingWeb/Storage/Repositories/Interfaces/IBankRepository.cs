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
        /// Получить банк с настройками парсинга.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Bank> GetWithSettings(int id);
    }
}

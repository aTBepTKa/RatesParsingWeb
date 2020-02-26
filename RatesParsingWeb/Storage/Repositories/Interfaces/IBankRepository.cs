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
        /// Получить банк с командами обработки текста.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Bank> GetBankCommands(int id);
    }
}

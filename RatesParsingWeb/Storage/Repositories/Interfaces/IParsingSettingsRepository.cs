using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories.Interfaces
{
    public interface IParsingSettingsRepository : IRepository<ParsingSettings>
    {
        /// <summary>
        /// Получить команды для обработки текста.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ParsingSettings> GetCommands(int id);
    }
}

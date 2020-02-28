using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.ParsingSettings;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IParsingSettingsService : ICrudService<ParsingSettingsDto>
    {
        /// <summary>
        /// Получить настройки парсинга по Id банка.
        /// </summary>
        /// <param name="id">Id банка.</param>
        /// <returns></returns>
        Task<ParsingSettingsDto> GetSettingsByBankId(int id);
    }
}
using RatesParsingWeb.Dto.Bank;
using RatesParsingWeb.Dto.ParsingService;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    /// <summary>
    /// Средства для работы с модулем парсинга.
    /// </summary>
    public interface IParsingService
    {
        Task<ParsingResultDto> GetExchangeRates(ParsingSettingsDto parsingSettings, string taskName = "Задание");
    }
}

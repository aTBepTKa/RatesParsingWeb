using ParsingMessages;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.ParsingService;
using RatesParsingWeb.Dto.ParsingSettings;
using System;
using System.Collections.Generic;
using System.Linq;
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

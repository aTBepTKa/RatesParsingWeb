using RatesParsingWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IExchangeRateService : ICrudService<ExchangeRateDto>
    {
        Task<IEnumerable<ExchangeRateDto>> GetExchangeRatesAsync(int id);
    }
}

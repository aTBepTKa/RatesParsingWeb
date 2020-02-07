using RatesParsingWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IExchangeRateListService : ICrudService<ExchangeRateListDto>
    {
        /// <summary>
        /// Получить обменные курсы валют по дням для банка.
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        Task<IEnumerable<ExchangeRateListDto>> GetBankExchangeRateLists(int bankId);
    }
}

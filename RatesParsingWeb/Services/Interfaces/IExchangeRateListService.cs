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
        /// Получить список обменных курсов по дням для заданного банка.
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        Task<IEnumerable<ExchangeRateListDto>> GetBankExchangeRateLists(int bankId);

        /// <summary>
        /// Получить список последних обменных курсов для каждого банка.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ExchangeRateListDto> GetLastExchangeRateLists();
    }
}

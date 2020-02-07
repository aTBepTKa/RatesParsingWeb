using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;

namespace RatesParsingWeb.Services
{
    public class ExchangeRateListService : BaseCrudService<ExchangeRateListDto, ExchangeRateList>, IExchangeRateListService
    {
        private readonly IExchangeRateListRepository rateListRepository;
        
        public ExchangeRateListService(IExchangeRateListRepository exchangeRate) : base(exchangeRate)
        {
            rateListRepository = exchangeRate;
        }

        public async Task<IEnumerable<ExchangeRateListDto>> GetBankExchangeRateLists(int bankId)
        {
            var exchangeRateList = await rateListRepository.GetManyAsync(i => i.BankId == bankId);
            return exchangeRateList.Adapt<IEnumerable<ExchangeRateListDto>>();
        }
    }
}

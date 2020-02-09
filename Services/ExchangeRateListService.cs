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
        private readonly IExchangeRateListRepository listRepository;

        public ExchangeRateListService(IExchangeRateListRepository exchangeRate) : base(exchangeRate)
        {
            listRepository = exchangeRate;
        }

        public async Task<IEnumerable<ExchangeRateListDto>> GetBankExchangeRateLists(int bankId)
        {
            var exchangeRateList = await listRepository.GetManyAsync(i => i.BankId == bankId);
            return exchangeRateList.Adapt<IEnumerable<ExchangeRateListDto>>();
        }

        public  IEnumerable<ExchangeRateListDto> GetLastExchangeRateLists()
        {
            // Не работает в EF .Core 3.0.
            //var listQuery = listRepository.Query()                
            //    .GroupBy(i => i.BankId)                
            //    .Select(g => g.OrderByDescending(i => i.DateTimeStamp).FirstOrDefault())
            //    .ToArray();

            var lists = listRepository.Query();
            var lastLists = lists
                .Select(i => i.BankId)
                .Distinct()
                .Select(bankId => lists
                    .OrderByDescending(i => i.DateTimeStamp)
                    .FirstOrDefault(i => i.BankId == bankId))
                .ToArray();
            return lastLists.Adapt<IEnumerable<ExchangeRateListDto>>();
        }
    }
}

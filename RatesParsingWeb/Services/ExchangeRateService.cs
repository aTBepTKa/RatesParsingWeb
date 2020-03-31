using Mapster;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class ExchangeRateService : BaseCrudService<Dto.ExchangeRateDto, Domain.ExchangeRate>, IExchangeRateService
    {
        private readonly IExchangeRateRepository rateRepository;
        public ExchangeRateService(IExchangeRateRepository exchangeRate) : base(exchangeRate)
        {
            rateRepository = exchangeRate;
        }

        public async Task<IEnumerable<Dto.ExchangeRateDto>> GetExchangeRates(int id)
        {
            var rates = await rateRepository.GetManyAsync(i => i.ExchangeRateListId == id, c => c.Currency);
            return rates.Adapt<IEnumerable<Dto.ExchangeRateDto>>();
        }
    }
}

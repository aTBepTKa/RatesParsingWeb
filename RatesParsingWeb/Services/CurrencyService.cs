using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Storage.Repositories;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class CurrencyService : BaseCrudService<CurrencyDto, Currency>, ICurrencyService
    {
        private readonly ICurrencyRepository currencyRepository;
        public CurrencyService(ICurrencyRepository repository) : base(repository)
        {
            currencyRepository = repository;
        }

        public CurrencyDto GetCurrencyByTextCode(string textCode) =>
            currencyRepository.GetFirstOrDefault(i => i.TextCode == textCode).Adapt<CurrencyDto>();
    }
}

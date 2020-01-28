using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Storage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class CurrencyService : ServiceBase<Currency>, ICurrencyService
    {
        public CurrencyService(BankRatesContext context)
        {
            RepositoryBase = new CurrencyRepository(context);
        }

        public override bool IsValid(Currency currency, ModelStateDictionary modelState)
        {
            return true;
        }
    }
}

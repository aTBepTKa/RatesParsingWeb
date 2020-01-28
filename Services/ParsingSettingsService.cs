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
    public class ParsingSettingsService : ServiceBase<ParsingSettings>, IParsingSettingsService
    {
        public ParsingSettingsService(BankRatesContext context)
        {
            RepositoryBase = new ParsingSettingsRepository(context);
        }

        public override bool IsValid(ParsingSettings parsingSettings, ModelStateDictionary modelState)
        {
            return true;
        }
    }
}

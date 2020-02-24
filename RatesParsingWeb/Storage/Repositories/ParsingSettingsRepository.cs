using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class ParsingSettingsRepository : RepositoryBase<ParsingSettings>, IParsingSettingsRepository
    {
        public ParsingSettingsRepository(BankRatesContext context) : base(context)
        {
        }
    }
}

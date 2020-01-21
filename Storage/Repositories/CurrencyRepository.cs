using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using RatesParsingWeb.Domain;

namespace RatesParsingWeb.Storage.Repositories
{
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrency
    {
        public CurrencyRepository(BankRatesContext context) : base(context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using RatesParsingWeb.Domain;
using System.Linq.Expressions;

namespace RatesParsingWeb.Storage.Repositories
{
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(BankRatesContext context) : base(context)
        { }
    }
}

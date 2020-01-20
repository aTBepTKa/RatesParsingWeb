using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public interface IBankRepository : IRepository<Bank>
    {
        IEnumerable<Currency> GetCurrencies();

    }
}

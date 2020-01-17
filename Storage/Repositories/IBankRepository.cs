using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public interface IBankRepository
    {
        Task<List<Bank>> GetBankListAsync();
        Task<Bank> GetBankByIdAsync(int? id);
        IEnumerable<Currency> GetCurrencies();
    }
}

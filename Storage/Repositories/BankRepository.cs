using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(DbContextOptions<BankRatesContext> options) :
            base(options)
        { }

        public async Task<List<Bank>> GetBankListAsync() =>
             await context.Banks
                .Include(b=>b.Currency).ToListAsync();

        public async Task<Bank> GetBankByIdAsync(int? id) =>
            await context.Banks
                .Include(b => b.Currency).FirstOrDefaultAsync(m => m.Id == id);

        public IEnumerable<Currency> GetCurrencies() =>
            context.Currencies;
    }
}

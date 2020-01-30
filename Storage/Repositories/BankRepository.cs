using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using RatesParsingWeb.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(BankRatesContext context) :
            base(context)
        { }

        // TODO: Разобраться с ConfigureAwait (шо цэ за хуйня).
        public async Task<IEnumerable<Bank>> GetAllWithCurrenciesAsync() =>            
            await dbSet.Include(i => i.Currency).ToArrayAsync().ConfigureAwait(true); 

        public override async Task<Bank> GetByIdAsync(int id) =>
            await // Почему здесь async/await не нужен?
            dbSet
            .Include(i => i.Currency)
            .FirstOrDefaultAsync(i => i.Id == id);

        public async Task<Bank> GetByIdAsync(int id, params Expression<Func<Bank, object>>[] expression) =>
            await dbSet.IncludeEntities(expression).FirstOrDefaultAsync(i => i.Id == id);

        public Task<Bank> GetByIdWithSettingsAcync(int id) =>            
            dbSet
            .Include(i => i.Currency)
            .Include(i => i.ParsingSettings)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}

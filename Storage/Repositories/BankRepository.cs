using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {        
        public BankRepository(BankRatesContext context) :
            base(context)
        { }

        /// <summary>
        /// Возвращает список банков с основной валютой банка.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Bank>> GetAll() =>
            await _dbSet.Include(i => i.Currency).AsNoTracking().ToListAsync();
        
        public IEnumerable<Currency> GetCurrencies() =>
            _context.Currencies.ToList();
    }
}

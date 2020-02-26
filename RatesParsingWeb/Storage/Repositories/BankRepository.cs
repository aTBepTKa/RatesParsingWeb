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

        public async Task<Bank> GetBankCommands(int id)
        {
            var bank = await dbSet
                .Include(i => i.ParsingSettings)
                    .ThenInclude(i => i.TextCodeCommands)
                .Include(i => i.ParsingSettings)
                    .ThenInclude(i => i.UnitCommands)
                .FirstOrDefaultAsync(i => i.Id == id);
            return bank;
        }

    }
}

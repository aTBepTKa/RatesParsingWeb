using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(BankRatesContext context) :
            base(context)
        { }

        public Task<Bank> GetWithSettings(int id)
        {
            var bank = dbSet
                .Include(bank => bank.Currency)
                .Include(bank => bank.ParsingSettings)
                    .ThenInclude(settings => settings.Commands)
                            .ThenInclude(command => command.CommandParameters)
                .Include(bank => bank.ParsingSettings)
                    .ThenInclude(settings => settings.Commands)
                        .ThenInclude(commands => commands.CommandFieldName)
                .FirstOrDefaultAsync(bank => bank.Id == id);
            return bank;
        }

    }
}

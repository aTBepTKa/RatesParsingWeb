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

        public Task<Bank> GetBankWithSettings(int id)
        {
            var bank = dbSet
                .Include(bank => bank.ParsingSettings)
                    .ThenInclude(settings => settings.Commands)
                        .ThenInclude(assignment => assignment.Command)
                            .ThenInclude(command => command.CommandParameters)
                .Include(bank => bank.ParsingSettings)
                    .ThenInclude(settings => settings.Commands)
                        .ThenInclude(assignment => assignment.CommandParameterValues)
                .Include(bank => bank.ParsingSettings)
                    .ThenInclude(settings => settings.Commands)
                        .ThenInclude(assignment => assignment.AssignmentFieldName)
                .FirstOrDefaultAsync(bank => bank.Id == id);
            return bank;
        }
    }
}

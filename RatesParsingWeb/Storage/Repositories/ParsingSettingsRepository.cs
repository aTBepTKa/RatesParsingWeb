using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class ParsingSettingsRepository : RepositoryBase<ParsingSettings>, IParsingSettingsRepository
    {
        public ParsingSettingsRepository(BankRatesContext context) : base(context)
        {
        }

        public async Task<ParsingSettings> GetCommands(int id)
        {
            var commands = await dbSet
                .Include(settings => settings.Commands)
                    .ThenInclude(assignment => assignment.Command)
                        .ThenInclude(command => command.CommandParameters)
                .Include(settings => settings.Commands)
                    .ThenInclude(assignment => assignment.CommandParameterValues)
                .Include(settings => settings.Commands)
                    .ThenInclude(assignment => assignment.AssignmentFieldName)
                .FirstOrDefaultAsync(settings => settings.Id == id);

            return commands;
        }
    }
}

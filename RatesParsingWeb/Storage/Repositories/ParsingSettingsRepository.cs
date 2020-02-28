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

        public ParsingSettings GetCommands(int id)
        {
            var commands = dbSet
                .Include(settings => settings.TextCodeCommands)
                    .ThenInclude(assignment => assignment.Command)
                .Include(settings => settings.TextCodeCommands)
                    .ThenInclude(assignment => assignment.CommandParameters)

                .Include(settings => settings.UnitCommands)
                    .ThenInclude(assignment => assignment.Command)
                .Include(settings => settings.UnitCommands)
                    .ThenInclude(assignment => assignment.CommandParameters)

                .FirstOrDefault(settings => settings.Id == id);
            
            return commands;
        }
    }
}

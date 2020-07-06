using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class CommandParameterRepository:RepositoryBase<CommandParameter>, ICommandParameterRepository
    {
        public CommandParameterRepository(BankRatesContext context) : base(context)
        {
        }
    }
}

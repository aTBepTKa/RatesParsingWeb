using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories.Interfaces
{
    public interface IParsingSettingsRepository : IRepository<ParsingSettings>
    {
        ParsingSettings GetCommands(int id);
    }
}

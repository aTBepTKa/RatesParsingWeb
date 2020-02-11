using Mapster;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class ScriptService : BaseCrudService<ScriptDto, Script>, IScriptService
    {
        private readonly IScriptRepository scriptRepository;

        public ScriptService(IScriptRepository script) : base(script)
        {
            scriptRepository = script;
        }

        public async Task<IEnumerable<ScriptDto>> GetScriptsWithParameters() =>
            (await scriptRepository.GetAllAsync(i => i.ScriptParameters)).Adapt<IEnumerable<ScriptDto>>();
    }
}

using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Storage.Repositories;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class ParsingSettingsService : BaseCrudService<ParsingSettingsDto, ParsingSettings>, IParsingSettingsService
    {
        private readonly IParsingSettingsRepository settingsRepository;
        private readonly IBankRepository bankRepository;
        public ParsingSettingsService(IParsingSettingsRepository settings, IBankRepository bank) : base(settings)
        {
            settingsRepository = settings;
            bankRepository = bank;
        }

        public async Task<ParsingSettingsDto> GetSettingsByBankId(int id)
        {
            var bank = await bankRepository.GetFirstOrDefaultAsync(b => b.Id == id, i => i.ParsingSettings);
            var settingsId = bank.ParsingSettings.Id;
            var settings = await settingsRepository.GetCommands(settingsId);
            var settingsDto = settings.Adapt<ParsingSettingsDto>();
            return settingsDto;
        }
    }
}

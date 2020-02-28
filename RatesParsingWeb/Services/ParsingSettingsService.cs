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
        public ParsingSettingsService(IParsingSettingsRepository repository) : base(repository)
        {
            settingsRepository = repository;
        }
    }
}

using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParsingMessages;
using System;
using RatesParsingWeb.Dto.ParsingService;

namespace RatesParsingWeb.Pages.Service
{
    public class IndexModel : PageModel
    {

        private readonly IBankService bankService;
        private readonly IParsingSettingsService parsingSettingsService;
        private readonly IParsingService parsingService;

        public IndexModel(IBankService bank, IParsingSettingsService settings, IParsingService parsingService)
        {
            bankService = bank;
            parsingSettingsService = settings;
            this.parsingService = parsingService;
        }

        public IEnumerable<ExchangeRateModel> ExchangeRateModels { get; set; }
        public ExchangeRateModel FirstExchangeRateModel { get; set; }

        public async Task OnGet()
        {
            var bank = await bankService.GetBankBySwiftCode("NBPLPLPWBAN");
            var parsingSettings = await parsingSettingsService.GetSettingsByBankId(bank.Id);
            var response = await parsingService.GetExchangeRates(parsingSettings, "Получить список валют для польского банка");

            ExchangeRateModels = response.Adapt<IEnumerable<ExchangeRateModel>>();
        }
    }
}

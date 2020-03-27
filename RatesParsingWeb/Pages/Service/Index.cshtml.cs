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
        private readonly IParsingService parsingService;

        public IndexModel(IBankService bank, IParsingService parsingService)
        {
            bankService = bank;
            this.parsingService = parsingService;
        }

        public IEnumerable<ExchangeRateModel> ExchangeRateModels { get; set; }
        public ExchangeRateModel FirstExchangeRateModel { get; set; }

        public async Task OnGet()
        {
            var bank = await bankService.GetBankWithParsingSettings("NBPLPLPWBAN");
            var response = await parsingService.GetExchangeRates(bank.ParsingSettings, "Получить список валют для польского банка");
            ExchangeRateModels = response.Adapt<IEnumerable<ExchangeRateModel>>();
        }
    }
}

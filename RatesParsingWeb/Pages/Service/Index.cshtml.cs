using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RatesParsingWeb.Pages.Service
{
    public class IndexModel : PageModel
    {

        private readonly IBankService bankService;
        private readonly IParsingService parsingService;

        public IndexModel(IBankService bank, IParsingService parsing)
        {
            bankService = bank;
            parsingService = parsing;
        }

        public ParsingResultModel ParsingResultModel { get; set; }
        public ExchangeRateModel FirstExchangeRateModel { get; set; }

        public async Task OnGet()
        {
            var bank = await bankService.GetWithParsingSettingsAsync("NBPLPLPWBAN");
            var response = await parsingService.GetExchangeRates(bank.ParsingSettings, "Получить список валют для польского банка");
            ParsingResultModel = response.Adapt<ParsingResultModel>();
            if(ParsingResultModel.IsSuccesfullParsed)
                FirstExchangeRateModel = ParsingResultModel.ExchangeRates.FirstOrDefault();
        }
    }
}

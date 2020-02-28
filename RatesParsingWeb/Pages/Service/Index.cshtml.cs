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
        private readonly IBusControl busControl;
        private readonly IBankService bankService;
        private readonly IParsingSettingsService parsingService;
        public IndexModel(IBusControl bus, IBankService bank,IParsingSettingsService settings)
        {
            busControl = bus;
            bankService = bank;
            parsingService = settings;
        }

        public IEnumerable<ExchangeRateModel> ExchangeRateModels { get; set; }

        public async Task OnGet()
        {
            var serviceAddress = new Uri("rabbitmq://localhost/ParsingQueue");
            var client = busControl.CreateRequestClient<IParsingRequest>(serviceAddress);

            var bank = await bankService.GetBankBySwiftCode("NBPLPLPWBAN");
            var parsingSettings = await parsingService.GetSettingsByBankId(bank.Id);
            var request = parsingSettings.Adapt<ParsingRequest>();

            var response = await client.GetResponse<IParsingResponse>(request);
            ExchangeRateModels = response.Message.ExchangeRates.Adapt<IEnumerable<ExchangeRateModel>>();
        }
    }
}

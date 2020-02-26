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
        public IndexModel(IBusControl bus, IBankService bank)
        {
            busControl = bus;
            bankService = bank;
        }

        public IEnumerable<ExchangeRateModel> ExchangeRateModels { get; set; }

        public async Task OnGet()
        {
            var serviceAddress = new Uri("rabbitmq://localhost/ParsingQueue");
            var client = busControl.CreateRequestClient<IParsingRequest>(serviceAddress);

            int bankId = 1;
            var parsingSettings = bankService.GetBankParsingSettings(bankId);
            var request = parsingSettings.Adapt<ParsingRequest>();

            var response = await client.GetResponse<IParsingResponse>(request);
            ExchangeRateModels = response.Message.ExchangeRates.Adapt<IEnumerable<ExchangeRateModel>>();
        }
    }
}

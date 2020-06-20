using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Pages.Banks.ExchangeRateLists.ExchangeRates
{
    public class IndexModel : PageModel
    {
        private readonly IBankService bankService;
        private readonly IExchangeRateListService listService;
        private readonly IExchangeRateService rateService;

        public IndexModel(IExchangeRateService exchangeRate, IExchangeRateListService list, IBankService bank)
        {
            bankService = bank;
            listService = list;
            rateService = exchangeRate;
        }

        public BankModel BankModel { get; set; }
        public ExchangeRateListModel ExchangeRateListModel { get; set; }
        public List<ExchangeRateModel> ExchangeRateModels { get; set; }
        public ExchangeRateModel FirstExchangeRateModel { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var list = await listService.GetByIdAsync(id);
            if (list == null)
                return NotFound();
            ExchangeRateListModel = list.Adapt<ExchangeRateListModel>();

            var bank = await bankService.GetByIdAsync(list.BankId);
            if (bank == null)
                return NotFound();
            BankModel = bank.Adapt<BankModel>();

            var rates = await rateService.GetExchangeRatesAsync(id);

            if (rates.Any())
            {
                ExchangeRateModels = rates.Adapt<List<ExchangeRateModel>>();
            }
            return Page();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Models;
using Mapster;

namespace RatesParsingWeb.Pages.Banks.ExchangeRateLists
{
    public class IndexModel : PageModel
    {
        private readonly IExchangeRateListService listService;
        private readonly IBankService bankService;

        public IndexModel(IExchangeRateListService list, IBankService bank)
        {
            listService = list;
            bankService = bank;
        }

        public List<ExchangeRateListModel> ExchangeRateLists { get; set; }
        public BankModel BankModel { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var bank = await bankService.GetByIdAsync(id);
            if (bank == null)
                return NotFound();
            BankModel = bank.Adapt<BankModel>();

            var list = await listService.GetBankExchangeRateLists(id);
            if (list.Any())
                ExchangeRateLists = new List<ExchangeRateListModel>(list.Adapt<IEnumerable<ExchangeRateListModel>>());
            return Page();
        }
    }
}
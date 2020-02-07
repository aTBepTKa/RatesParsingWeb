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

        public async Task<IActionResult> OnGet(int bankId)
        {
            var bankDto = await bankService.GetByIdAsync(bankId);
            if (bankDto == null)
                return NotFound();
            BankModel = bankDto.Adapt<BankModel>();

            var listDto = await listService.GetBankExchangeRateLists(bankId);
            if (listDto.Any())
                ExchangeRateLists = new List<ExchangeRateListModel>(listDto.Adapt<IEnumerable<ExchangeRateListModel>>());
            return Page();
        }
    }
}
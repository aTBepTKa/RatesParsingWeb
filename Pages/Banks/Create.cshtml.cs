using System;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.App_Code;

namespace RatesParsingWeb.Pages.Banks
{
    public class CreateModel : BaseBankPageModel
    {
        private readonly IBankService bankService;
        private readonly ICurrencyService currencyService;

        public CreateModel(IBankService bank, ICurrencyService currency)
        {
            bankService = bank;
            currencyService = currency;
        }

        public async Task<IActionResult> OnGet()
        {
            await SetCurrencySelectListAsync(null, currencyService);
            return Page();
        }

        [BindProperty]
        public BankModel BankModel { get; set; }
                                                                                                                                                                                          
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetCurrencySelectListAsync(null, currencyService);
                return Page();
            }
            var bankCreateDto = BankModel.Adapt<BankCreateDto>();
            if (!await bankService.CreateBankAsync(bankCreateDto))
            {
                await SetCurrencySelectListAsync(null, currencyService);
                ValidationErrorList = bankService.ValidationDictionary.GetErrorListWithoutKeys();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

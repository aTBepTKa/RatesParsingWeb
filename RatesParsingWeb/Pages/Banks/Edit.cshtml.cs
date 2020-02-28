using System;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.UpdateAndCreate;

namespace RatesParsingWeb.Pages.Banks
{
    // ДОПИЛИТЬ:
    // - сделать модель для селектЛист
    // - разобраться с внешними id.
    public class EditModel : BaseBankPageModel
    {
        private readonly IBankService bankService;
        private readonly ICurrencyService currencyService;

        public EditModel(IBankService bank, ICurrencyService currency)
        {
            bankService = bank;
            currencyService = currency;
        }

        [BindProperty]
        public BankModel BankModel { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetBankAllData(id);
            if (bankDto == null)
                return NotFound();
            BankModel = bankDto.Adapt<BankModel>();
            await SetCurrencySelectListAsync(BankModel.CurrencyId, currencyService);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetCurrencySelectListAsync(BankModel.CurrencyId, currencyService);
                return Page();
            }
            var bankUpdateDto = BankModel.Adapt<BankUpdateDto>();

            if (!await bankService.UpdateAsync(bankUpdateDto))
            {
                ValidationErrorList = bankService.ValidationDictionary.ErrorListWithoutKeys;
                await SetCurrencySelectListAsync(BankModel.CurrencyId, currencyService);
                return Page();
            }
            return RedirectToPage("./Index");
        }


    }
}

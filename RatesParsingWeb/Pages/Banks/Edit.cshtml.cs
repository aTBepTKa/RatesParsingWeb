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
    public class EditModel : BaseBankEditPageModel
    {
        private readonly IBankService bankService;

        public EditModel(IBankService bankService,
                         ICurrencyService currencyService,
                         ICommandService commandService) : base(currencyService, commandService)
        {
            this.bankService = bankService;
        }

        [BindProperty]
        public BankModel BankModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetWithParsingSettingsAsync(id);
            if (bankDto == null)
                return NotFound();
            BankModel = bankDto.Adapt<BankModel>();
            await SetSeletListsAsync(BankModel.CurrencyId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool isSaveAction = true)
        {
            if (!ModelState.IsValid || !isSaveAction)
            {
                await SetSeletListsAsync(BankModel.CurrencyId);
                return Page();
            }
            var bankUpdateDto = BankModel.Adapt<BankUpdateDto>();

            if (!await bankService.UpdateAsync(bankUpdateDto))
            {
                ValidationErrorList = bankService.ValidationService.ErrorListWithoutKeys;
                await SetSeletListsAsync(BankModel.CurrencyId);
                return Page();
            }
            return RedirectToPage("./Index");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Models;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using RatesParsingWeb.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Dto;

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

        public SelectList CurrencySelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetBankAsync(id);
            if (bankDto == null)
                return NotFound();
            BankModel = MapDtoToModel(bankDto);

            CurrencySelectList = await GetCurrencySelectListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            BankDto bankDto = MapModelToDto(BankModel);
            if (! await bankService.UpdateBankAsync(bankDto))
                return Page();
            return RedirectToPage("./Index");
        }

        private async Task<SelectList> GetCurrencySelectListAsync()
        {
            IEnumerable<CurrencyDto> currenciesDto = await currencyService.GetAllAsync();
            IEnumerable<CurrencyModel> currenciesModel = currenciesDto.Adapt<IEnumerable<CurrencyModel>>();

            var selectList = new SelectList(currenciesModel, "Id", "CodeWithName");
            return selectList;
        }
    }
}

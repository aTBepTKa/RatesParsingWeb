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

        public SelectList CurrencySelectList => GetCurrencySelectListAsync(BankModel.CurrencyId).Result;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetById(id);            
            if (bankDto == null)
                return NotFound();
            BankModel = bankDto.Adapt<BankModel>();
            return Page();            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var bankUpdateDto = BankModel.Adapt<BankUpdateDto>();
            
            if (!await bankService.UpdateBankAsync(bankUpdateDto))
            {
                AddModelErrors(bankService.ModelState.ErrorDictioanry);
                return Page();
            }
            return RedirectToPage("./Index");
        }

        private async Task<SelectList> GetCurrencySelectListAsync(int SelectedId)
        {
            var currenciesDto = (await currencyService.GetAllAsync()).OrderBy(i => i.TextCode);
            var currenciesSelectList = currenciesDto.Adapt<IEnumerable<CurrencySelectListModel>>();

            var selectList = new SelectList(currenciesSelectList,
                                            nameof(CurrencySelectListModel.Id),
                                            nameof(CurrencySelectListModel.CodeWithName),
                                            currenciesSelectList.Single(i=>i.Id == SelectedId));
            return selectList;
        }

        private void AddModelErrors(IDictionary<string,string> errors)
        {
            foreach(var error in errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}

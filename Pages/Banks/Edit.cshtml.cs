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
        private BankDto BankDto { get; set; }

        public SelectList CurrencySelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto = await bankService.GetByIdAsync(id);
            if (BankDto == null)
                return NotFound();
            BankModel = GetBankModel(BankDto);

            CurrencySelectList = await GetCurrencySelectListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //if (! await BankExists(BankModel.Id))
            //{
            //    return NotFound();
            //}

            BankDto = BankModel.Adapt<BankDto>();
            if (! await bankService.UpdateBankAsync(BankDto))
                return Page();



            return RedirectToPage("./Index");
        }

        //private async Task<bool> BankExists(int id) =>
        //    await bankService.AnyAsync(i => i.Id == id);


        private async Task<SelectList> GetCurrencySelectListAsync()
        {
            IEnumerable<CurrencyModel> currenciesDto = (await currencyService.GetAll()).Adapt<IEnumerable<CurrencyModel>>();
            var currenciesModel = currenciesDto.Adapt<IEnumerable<CurrencyModel>>();
            var selectList = new SelectList(currenciesModel, "Id", "CodeWithName");
            return selectList;
        }
    }
}

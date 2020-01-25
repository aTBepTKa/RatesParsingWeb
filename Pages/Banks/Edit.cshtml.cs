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
using Mapster;

namespace RatesParsingWeb.Pages.Banks
{
    public class EditModel : BaseBankPageModel
    {
        private readonly IBankRepository bankRepository;
        private readonly ICurrencyRepository currencyRepository;

        public EditModel(IBankRepository bank, ICurrencyRepository currency)
        {
            bankRepository = bank;
            currencyRepository = currency;
        }

        [BindProperty]
        public BankModel BankModel { get; set; }
        private Bank BankDomain { get; set; }

        public SelectList CurrencySelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDomain = await bankRepository.GetByIdAsync(id);
            if (BankDomain == null)
                return NotFound();
            BankModel = GetBankModel(BankDomain);

            CurrencySelectList = await GetCurrencySelectListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!await BankExists(BankModel.Id))
            {
                return NotFound();
            }

            BankDomain = BankModel.Adapt<Bank>();
            bankRepository.SetStateModifed(BankDomain);
            await bankRepository.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task<bool> BankExists(int id) =>
            await bankRepository.AnyAsync(i => i.Id == id);


        private async Task<SelectList> GetCurrencySelectListAsync()
        {
            IEnumerable<Currency> currenciesDomain = await currencyRepository.GetAll();
            var currenciesModel = currenciesDomain.Adapt<IEnumerable<CurrencyModel>>();
            var selectList = new SelectList(currenciesModel, "Id", "CodeWithName");
            return selectList;
        }
    }
}

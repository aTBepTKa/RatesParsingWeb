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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDomain = await bankRepository.GetByIdAsync(id);
            if (BankDomain == null)
            {
                return NotFound();
            }
            BankModel = GetBankModel(BankDomain);

            // Сформировать список валюты для выпадающего списка.            
            IEnumerable<Currency> currenciesDomain = await currencyRepository.GetAll();            
            var currenciesModel = new List<CurrencyModel>(currenciesDomain.Count());
            foreach (var currencyDomain in currenciesDomain)
            {
                var newCurrencyModel = currencyDomain.Adapt<CurrencyModel>();
                currenciesModel.Add(newCurrencyModel);
            }
            ViewData["CurrencyID"] = new SelectList(currenciesModel, "Id", "CodeWithName");
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

        private async Task<bool> BankExists(int id)
        {
            return await bankRepository.AnyAsync(i => i.Id == id);
        }
    }
}

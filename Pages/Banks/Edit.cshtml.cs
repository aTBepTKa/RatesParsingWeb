using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories;
using RatesParsingWeb.Models;
using RatesParsingWeb.Pages.Banks;
using RatesParsingWeb.Storage.Repositories.Interfaces;

namespace RatesParsingWeb
{
    public class EditModel : BaseBankPageModel
    {
        private readonly IBankRepository _context;

        public EditModel(IBankRepository context)
        {
            _context = context;
        }

        [BindProperty]
        public BankModel BankModel { get; set; }
        private Bank BankDomain { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BankDomain = await _context.GetByIdAsync(id);
            BankModel = GetBankModel(BankDomain);

            if (BankModel == null)
            {
                return NotFound();
            }
            ViewData["CurrencyID"] = new SelectList(_context.GetCurrencies(), "Id", "TextCode");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SetStateModifed(BankDomain);
            SetModelToDomain(BankModel, BankDomain);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await BankExists(BankModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task <bool> BankExists(int id)
        {
            return await _context.AnyAsync(i => i.Id == id);
        }

        private void SetModelToDomain(BankModel model, Bank domain)
        {
            domain.Name = model.Name;
            domain.BankUrl = model.BankUrl;
            domain.Currency = _context.GetCurrencies().FirstOrDefault(i => i.Id == model.Id);
            domain.RatesUrl = model.RatesUrl;
        }
    }
}

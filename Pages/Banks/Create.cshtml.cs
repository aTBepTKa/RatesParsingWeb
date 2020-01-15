using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage;

namespace RatesParsingWeb
{
    public class CreateModel : PageModel
    {
        private readonly RatesParsingWeb.Storage.BankRatesContext _context;

        public CreateModel(RatesParsingWeb.Storage.BankRatesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CurrencyID"] = new SelectList(_context.Currencies, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Bank Bank { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Banks.Add(Bank);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

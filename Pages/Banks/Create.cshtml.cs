using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Data;
using RatesParsingWeb.Domain;

namespace RatesParsingWeb
{
    public class CreateModel : PageModel
    {
        private readonly RatesParsingWeb.Data.BankRatesContext _context;

        public CreateModel(RatesParsingWeb.Data.BankRatesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "Id");
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

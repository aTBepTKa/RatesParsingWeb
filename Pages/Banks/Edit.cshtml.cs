using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Data;
using RatesParsingWeb.Domain;

namespace RatesParsingWeb
{
    public class EditModel : PageModel
    {
        private readonly RatesParsingWeb.Data.BankRatesContext _context;

        public EditModel(RatesParsingWeb.Data.BankRatesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bank Bank { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bank = await _context.Banks
                .Include(b => b.Currency).FirstOrDefaultAsync(m => m.Id == id);

            if (Bank == null)
            {
                return NotFound();
            }
           ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "Id");
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

            _context.Attach(Bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(Bank.Id))
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

        private bool BankExists(int id)
        {
            return _context.Banks.Any(e => e.Id == id);
        }
    }
}

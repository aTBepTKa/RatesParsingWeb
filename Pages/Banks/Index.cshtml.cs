using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Data;
using RatesParsingWeb.Domain;

namespace RatesParsingWeb
{
    public class IndexModel : PageModel
    {
        private readonly RatesParsingWeb.Data.BankRatesContext _context;

        public IndexModel(BankRatesContext context)
        {
            _context = context;
        }

        public IList<Bank> Bank { get;set; }

        public async Task OnGetAsync()
        {
            Bank = await _context.Banks
                .Include(b => b.Currency).ToListAsync();
        }
    }
}

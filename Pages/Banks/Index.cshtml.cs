using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories;

namespace RatesParsingWeb
{
    public class IndexModel : PageModel
    {
        private readonly IBankRepository _context;

        public IndexModel(IBankRepository context)
        {
            _context = context;
        }

        public IList<Bank> Bank { get;set; }

        public async Task OnGetAsync()
        {
            Bank = await _context.GetBankListAsync();
        }
    }
}

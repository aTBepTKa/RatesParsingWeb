using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Models;
using RatesParsingWeb.Pages.Banks;

namespace RatesParsingWeb
{
    public class IndexModel : BaseBankPageModel
    {
        private readonly IBankRepository _context;

        public IndexModel(IBankRepository context)
        {
            _context = context;
        }

        public List<BankModel> Banks { get;set; }

        public async Task OnGetAsync()
        {
            var banksRepo = await _context.GetAll();
            foreach(var item in banksRepo)            
                Banks.Add(GetBankModel(item));            
        }
    }
}

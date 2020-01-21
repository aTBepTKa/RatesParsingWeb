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
using RatesParsingWeb.Storage.Repositories.Interfaces;

namespace RatesParsingWeb
{
    public class IndexModel : BaseBankPageModel
    {
        private readonly IBankRepository _context;

        public IndexModel(IBankRepository context)
        {
            _context = context;
        }

        public List<BankModel> Banks { get; set; }

        public async Task OnGetAsync()
        {
            var banksDomain = await _context.GetAll();
            if (banksDomain != null)
            {
                Banks = new List<BankModel>(banksDomain.Count());
                foreach (var item in banksDomain)
                    Banks.Add(GetBankModel(item));
            }
        }
    }
}

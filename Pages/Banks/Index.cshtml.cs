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
        private readonly IBankRepository bankRepository;

        public IndexModel(IBankRepository context)
        {
            bankRepository = context;
        }

        public List<BankModel> Banks { get; set; }
        public BankModel FirstBankObject { get;  set; }

        public async Task OnGetAsync()
        {
            IEnumerable<Bank> banksDomain = await bankRepository.GetAll();
            if (banksDomain != null && banksDomain.Any())
            {
                Banks = new List<BankModel>(banksDomain.Count());
                foreach (var item in banksDomain)
                    Banks.Add(GetBankModel(item));
                FirstBankObject = Banks[0];
            }
        }
    }
}

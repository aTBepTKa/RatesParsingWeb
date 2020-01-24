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
using RatesParsingWeb.Storage.Repositories.Interfaces;
using Mapster;

namespace RatesParsingWeb.Pages.Banks
{
    public class IndexModel : BaseBankPageModel
    {
        private readonly IBankRepository bankRepository;

        public IndexModel(IBankRepository context)
        {
            bankRepository = context;
        }

        public List<BankModel> BanksModelList { get; set; }
        public BankModel FirstBankObject { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<Bank> banksDomain = await bankRepository.GetAll();
            if (banksDomain.Any())
            {
                BanksModelList = banksDomain.Adapt<List<BankModel>>();
                FirstBankObject = BanksModelList[0];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Models;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using RatesParsingWeb.Services.Interfaces;
using Mapster;

namespace RatesParsingWeb.Pages.Banks
{
    public class DetailsModel : BaseBankPageModel
    {
        private readonly IBankService bankService;

        public DetailsModel(IBankService bank)
        {
            bankService = bank;
        }

        public BankModel BankModel { get; set; }
        public ParsingSettingsModel ParsingSettingsModel { get; set; }
        private Bank BankDomain { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDomain = await bankService.GetByIdWithSettings(id);
            if (BankDomain == null)
                return NotFound();
            BankModel = GetBankModel(BankDomain);

            if (BankDomain.ParsingSettings == null)
            {
                return Page();
            }

            ParsingSettingsModel = BankDomain.ParsingSettings.Adapt<ParsingSettingsModel>();

            return Page();
        }
    }
}

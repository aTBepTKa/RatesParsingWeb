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
using RatesParsingWeb.Dto;

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
        private BankDto BankDto { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //BankDomain = await bankService.GetByIdWithSettings(id);
            BankDto = await bankService.GetByIdAsync(id);
            if (BankDto == null)
                return NotFound();
            BankModel = GetBankModel(BankDto);

            //if (BankDto.ParsingSettings == null)
            //{
            //    return Page();
            //}

            //ParsingSettingsModel = BankDto.ParsingSettings.Adapt<ParsingSettingsModel>();

            return Page();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using Mapster;
using RatesParsingWeb.Dto.Bank;

namespace RatesParsingWeb.Pages.Banks
{
    public class DetailsModel : PageModel
    {
        private readonly IBankService bankService;

        public DetailsModel(IBankService bank)
        {
            bankService = bank;
        }

        public BankModel BankModel { get; set; }
        public IEnumerable<CommandModel> TextCodeCommands { get; set; }
        public IEnumerable<CommandModel> UnitCommands { get; set; }
        public IEnumerable<CommandModel> ExchangeRateCommands { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetWithParsingSettingsAsync(id);
            if (bankDto == null)
                return NotFound();
            BankModel = bankDto.Adapt<BankModel>();
            TextCodeCommands = BankModel.ParsingSettings.Commands.Where(c => c.CommandFieldName.Name == "TextCode");
            UnitCommands = BankModel.ParsingSettings.Commands.Where(c => c.CommandFieldName.Name == "Unit");
            ExchangeRateCommands = BankModel.ParsingSettings.Commands.Where(c => c.CommandFieldName.Name == "ExchangeRate");
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Pages.Banks.Commands
{
    public class IndexModel : PageModel
    {
        public BankModel BankModel { get; set; }
        public IEnumerable<CommandModel> TextCodeCommandsModel { get; set; }
        public IEnumerable<CommandModel> UnitCommandsModel { get; set; }
        public IEnumerable<CommandModel> ExchangeRateCommandsModel { get; set; }
        private readonly IBankService bankService;

        public IndexModel(IBankService bankService)
        {
            this.bankService = bankService;
        }
        public async Task<IActionResult> OnGet(int bankId)
        {
            var bankDto = await bankService.GetWithParsingSettingsAsync(bankId);
            if (bankDto == null)
                return NotFound();
            BankModel = bankDto.Adapt<BankModel>();
            
            TextCodeCommandsModel = BankModel.ParsingSettings.Commands.Where(x => x.CommandFieldName.Name == "TextCode");
            UnitCommandsModel = BankModel.ParsingSettings.Commands.Where(x => x.CommandFieldName.Name == "Unit");
            ExchangeRateCommandsModel = BankModel.ParsingSettings.Commands.Where(x => x.CommandFieldName.Name == "ExchangeRate");

            return Page();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Dto;
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
        public IEnumerable<CommandAssignmentModel> TextCodeCommands { get; set; }
        public IEnumerable<CommandAssignmentModel> UnitCommands { get; set; }
        public IEnumerable<CommandAssignmentModel> ExchangeRateCommands { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetWithParsingSettingsAsync(id);
            if (bankDto == null)
                return NotFound();
            BankModel = bankDto.Adapt<BankModel>();
            TextCodeCommands = BankModel.ParsingSettings.Commands.Where(c => c.AssignmentFieldName.Name == "TextCode");
            UnitCommands = BankModel.ParsingSettings.Commands.Where(c => c.AssignmentFieldName.Name == "Unit");
            ExchangeRateCommands = BankModel.ParsingSettings.Commands.Where(c => c.AssignmentFieldName.Name == "ExchangeRate");
            return Page();
        }
    }
}

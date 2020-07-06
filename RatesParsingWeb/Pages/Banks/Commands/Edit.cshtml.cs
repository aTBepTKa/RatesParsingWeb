using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using GreenPipes.Internals.Mapping;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Dto.Bank.Command;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Pages.Banks.Commands
{
    public class EditModel : PageModel
    {
        private readonly ICommandService commandService;
        public EditModel(ICommandService commandService)
        {
            this.commandService = commandService;
        }


        public CommandModel CommandModel { get; set; }

        [BindProperty]
        public int BankId { get; set; }
        [BindProperty]
        public CommandParameterValueModel[] CommandParameterValues { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int commandId, int bankId)
        {
            var commandDto = await commandService.GetCommandWithParameterAsync(commandId);
            if (commandDto == null)
                return NotFound();
            CommandModel = commandDto.Adapt<CommandModel>();
            BankId = bankId;
            CommandParameterValues = CommandModel.CommandParameters.Adapt<CommandParameterValueModel[]>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)            
                return Page();
            var parametersUpdateDto = CommandParameterValues.Adapt<CommandParameterUpdateDto[]>();
            if (!await commandService.UpdateParameterAsync(parametersUpdateDto))
            {
                return Page();
            }
            return RedirectToPage("./Index", new { bankId = BankId });
        }
    }
}
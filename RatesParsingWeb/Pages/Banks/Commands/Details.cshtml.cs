using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Pages.Banks.Commands
{
    public class DetailsModel : PageModel
    {
        private readonly ICommandService commandService;

        public DetailsModel(ICommandService command)
        {
            commandService = command;
        }

        public CommandAssignmentModel CommandAssignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var command = await commandService.GetCommandWithParameterAsync(id);
            if (command == null)
                return NotFound();
            CommandModel = command.Adapt<CommandModel>();
            return Page();
        }
    }
}
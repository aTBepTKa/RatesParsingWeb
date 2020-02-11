using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Pages.Commands
{
    public class IndexModel : PageModel
    {
        private readonly ICommandService commandService;

        public IndexModel(ICommandService command)
        {
            commandService = command;
        }

        public IEnumerable<CommandModel> CommandModels { get; set; }
        public CommandModel FirstCommandModelObject { get; set; }

        public async Task OnGet()
        {
            IEnumerable<CommandDto> commands = await commandService.GetCommandsWithParameters();
            if (commands.Any())
            {
                CommandModels = commands.Adapt<IEnumerable<CommandModel>>();
                FirstCommandModelObject = CommandModels.FirstOrDefault();
            }
        }
    }
}
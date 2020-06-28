using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Dto.CommandService;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            IEnumerable<CommandDto> commands = await commandService.GetCommandListWithParameterAsync();
            if (commands.Any())
            {
                CommandModels = commands.Adapt<IEnumerable<CommandModel>>();
                FirstCommandModelObject = CommandModels.FirstOrDefault();
            }
        }
    }
}
using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages.Commands
{
    public class BaseCommandEditPageModel : PageModel
    {
        protected readonly ICommandService commandService;

        protected BaseCommandEditPageModel(ICommandService commandService)
        {
            this.commandService = commandService;
        }

        public SelectList CommandSelectList { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }

        /// <summary>
        /// Задать выпадающий список для выбора команды.
        /// </summary>
        /// <param name="selectedCommand"></param>
        /// <returns></returns>
        protected async Task SetCommandSelectListAsync()
        {
            var commands = await GetExternalCommandsList();
            CommandSelectList = new SelectList(commands,
                                               nameof(CommandModel.Name),
                                               nameof(CommandModel.Name));
        }

        private async Task<IEnumerable<CommandModel>> GetExternalCommandsList()
        {
            var commands = await commandService.GetExternalCommands();
            if (commands.IsSuccesfullParsed)
                return commands.Message.Adapt<IEnumerable<CommandModel>>();
            else
                return Array.Empty<CommandModel>();
        }
    }
}

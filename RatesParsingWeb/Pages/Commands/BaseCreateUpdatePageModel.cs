using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages.Commands
{
    public class BaseCreateUpdatePageModel : PageModel
    {
        protected readonly ICommandService commandService;

        protected BaseCreateUpdatePageModel(ICommandService commandService)
        {
            this.commandService = commandService;
        }

        public SelectList CommandSelectList { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        /// <summary>
        /// Сформировать данные для выпадающего списка.
        /// </summary>
        protected void SetCommandSelectList(IEnumerable<CommandModel> commands, CommandModel selectedCommand = null)
        {
            CommandSelectList = new SelectList(commands,
                                               nameof(CommandModel.Name),
                                               nameof(CommandModel.Name),
                                               selectedCommand);
        }

        /// <summary>
        /// Получить список команд из внешнего приложения.
        /// </summary>
        /// <returns></returns>
        protected async Task<IEnumerable<CommandCreateDto>> GetExternalCommandsList()
        {
            var commands = await commandService.GetExternalCommands();
            if (commands.IsSuccesfullParsed)
                return commands.Commands.Adapt<IEnumerable<CommandCreateDto>>();
            else
                return Array.Empty<CommandCreateDto>();
        }
    }
}

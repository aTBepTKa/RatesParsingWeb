using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb
{
    public class CreateModel : PageModel
    {
        private readonly ICommandService commandService;

        public CreateModel(ICommandService command)
        {
            commandService = command;
        }

        [BindProperty]
        public CommandModel CommandModel { get; set; }
        public SelectList CommandSelectList { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }

        /// <summary>
        /// Как сделать чтобы при вызове метода OnGet, данные сохранялись в данном свойстве до конца работы с формой?
        /// Список 3 раза получаем в итоге (метод GetExternalCommands).
        /// </summary>
        private IEnumerable<CommandCreateDto> CommandsDto { get; set; }

        public void OnGet()
        {
            CommandsDto = commandService.GetExternalCommands();
            SetCommandSelectList();
        }

        /// <summary>
        /// Выбрать команду из списка для получения дополнительных сведений.
        /// </summary>
        public void OnPostCommand()
        {
            CommandsDto = commandService.GetExternalCommands();
            var command = CommandsDto.FirstOrDefault(i => i.Name == CommandModel.Name);
            CommandModel = command.Adapt<CommandModel>();
            SetCommandSelectList(CommandModel);
        }

        /// <summary>
        /// Сохранить команду.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            CommandsDto = commandService.GetExternalCommands();
            SetCommandSelectList();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CommandsDto = commandService.GetExternalCommands();
            var command = CommandsDto.FirstOrDefault(i => i.Name == CommandModel.Name);

            if (!await commandService.CreateAsync(command))
            {
                ValidationErrors = commandService.ValidationDictionary.ErrorListWithoutKeys;
                return Page();
            }
            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Сформировать данные для выпадающего списка.
        /// </summary>
        private void SetCommandSelectList(CommandModel selectedCommand = null)
        {
            var commandsModel = CommandsDto.Adapt<IEnumerable<CommandModel>>();
            CommandSelectList = new SelectList(commandsModel,
                                               nameof(CommandModel.Name),
                                               nameof(CommandModel.Name),
                                               selectedCommand);
        }


    }
}
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages.Commands
{
    public class CreateModel : BaseCreateUpdatePageModel
    {
        public CreateModel(ICommandService command) : base(command)
        { }

        [BindProperty]
        public CommandModel CommandModel { get; set; }

        public async Task OnGet()
        {
            var commands = (await GetExternalCommandsList()).Adapt<IEnumerable<CommandModel>>();
            SetCommandSelectList(commands);
        }

        /// <summary>
        /// Выбрать команду из списка для получения дополнительных сведений.
        /// </summary>
        public async Task<IActionResult> OnPostCommand()
        {
            var commands = (await GetExternalCommandsList()).Adapt<IEnumerable<CommandModel>>();
            if (!commands.Any())
                return Page();
            var command = commands.FirstOrDefault(i => i.Name == CommandModel.Name);
            CommandModel = command.Adapt<CommandModel>();
            SetCommandSelectList(commands, CommandModel);
            return Page();
        }

        /// <summary>
        /// Сохранить команду.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var commands = await GetExternalCommandsList();
            if (!commands.Any())
                return Page();

            SetCommandSelectList(commands.Adapt<IEnumerable<CommandModel>>(), CommandModel);
            if (!ModelState.IsValid)
                return Page();

            var command = commands.FirstOrDefault(i => i.Name == CommandModel.Name);

            if (!await commandService.CreateAsync(command))
            {
                ValidationErrors = commandService.ValidationDictionary.ErrorListWithoutKeys;
                CommandModel = command.Adapt<CommandModel>();
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages.Commands
{
    public class DeleteModel : PageModel
    {
        private readonly ICommandService commandService;
        public DeleteModel(ICommandService command)
        {
            commandService = command;
        }

        public CommandModel CommandModel { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var command = await commandService.GetByIdAsync(id);
            if (command == null)
                return NotFound();
            CommandModel = command.Adapt<CommandModel>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await commandService.DeleteAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
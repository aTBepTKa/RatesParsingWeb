using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public CommandModel  CommandModel { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var command = CommandModel.Adapt<CommandCreateDto>();

            if(!await commandService.CreateAsync(command))
            {
                ValidationErrors = commandService.ValidationDictionary.ErrorListWithoutKeys;
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
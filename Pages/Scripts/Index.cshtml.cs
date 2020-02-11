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

namespace RatesParsingWeb.Pages.Scripts
{
    public class IndexModel : PageModel
    {
        private readonly IScriptService scriptService;

        public IndexModel(IScriptService script)
        {
            scriptService = script;
        }

        public IEnumerable<ScriptModel> ScriptModels { get; set; }
        public ScriptModel FirstScriptModelObject { get; set; }

        public async Task OnGet()
        {
            IEnumerable<ScriptDto> scripts = await scriptService.GetScriptsWithParameters();
            if (scripts.Any())
            {
                ScriptModels = scripts.Adapt<IEnumerable<ScriptModel>>();
                FirstScriptModelObject = ScriptModels.FirstOrDefault();
            }
        }
    }
}
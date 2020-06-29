using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Pages.Banks;

namespace RatesParsingWeb.Pages.Shared.Components.CommandTable
{
    public class CommandTableViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(EditModel editModel)
        {
            return View(editModel);
        }
    }
}
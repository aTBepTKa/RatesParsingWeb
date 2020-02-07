using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage;

namespace RatesParsingWeb.Pages.Banks
{
    public class DeleteModel : PageModel
    {
        private readonly IBankService bankService;

        public DeleteModel(IBankService bank)
        {
            bankService = bank;
        }

        [BindProperty]
        public BankModel BankModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetByIdAsync(id);
            if (bankDto == null)
            {
                return NotFound();
            }
            BankModel = bankDto.Adapt<BankModel>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await bankService.DeleteAsync(id);
            return RedirectToPage("./Index");
        }
    }
}

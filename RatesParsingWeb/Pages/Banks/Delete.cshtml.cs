using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using System.Threading.Tasks;

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
            BankDto bankDto = await bankService.GetWithCurrencyAsync(id);
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

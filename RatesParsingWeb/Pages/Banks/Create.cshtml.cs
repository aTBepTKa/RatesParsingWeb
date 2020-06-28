using Mapster;
using Microsoft.AspNetCore.Mvc;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages.Banks
{
    public class CreateModel : BaseBankPageModel
    {
        private readonly IBankService bankService;
        private readonly ICurrencyService currencyService;

        public CreateModel(IBankService bank, ICurrencyService currency)
        {
            bankService = bank;
            currencyService = currency;
        }

        public async Task<IActionResult> OnGet()
        {
            await SetCurrencySelectListAsync(null, currencyService);
            return Page();
        }

        [BindProperty]
        public BankModel BankModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetCurrencySelectListAsync(null, currencyService);
                return Page();
            }
            var bankCreateDto = BankModel.Adapt<BankCreateDto>();
            if (!await bankService.CreateAsync(bankCreateDto))
            {
                await SetCurrencySelectListAsync(null, currencyService);
                ValidationErrorList = bankService.ValidationService.ErrorListWithoutKeys;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

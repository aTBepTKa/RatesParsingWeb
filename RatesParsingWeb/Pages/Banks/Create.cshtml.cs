using Mapster;
using Microsoft.AspNetCore.Mvc;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages.Banks
{
    public class CreateModel : BaseBankEditPageModel
    {
        private readonly IBankService bankService;

        public CreateModel(IBankService bankService,
                         ICurrencyService currencyService,
                         ICommandService commandService) : base(currencyService, commandService)
        {
            this.bankService = bankService;
        }

        public async Task<IActionResult> OnGet()
        {
            await SetCurrencySelectListAsync();
            return Page();
        }

        [BindProperty]
        public BankModel BankModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetCurrencySelectListAsync();
                return Page();
            }
            var bankCreateDto = BankModel.Adapt<BankCreateDto>();
            if (!await bankService.CreateAsync(bankCreateDto))
            {
                await SetCurrencySelectListAsync();
                ValidationErrorList = bankService.ValidationService.ErrorListWithoutKeys;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

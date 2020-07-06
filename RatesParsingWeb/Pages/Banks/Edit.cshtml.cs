using Mapster;
using Microsoft.AspNetCore.Mvc;
using RatesParsingWeb.Dto.Bank;
using RatesParsingWeb.Models;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages.Banks
{
    // ДОПИЛИТЬ:
    // - сделать модель для селектЛист
    // - разобраться с внешними id.
    public class EditModel : BaseBankEditPageModel
    {
        private readonly IBankService bankService;

        public EditModel(IBankService bankService,
                         ICurrencyService currencyService,
                         ICommandService commandService) : base(currencyService, commandService)
        {
            this.bankService = bankService;
        }

        [BindProperty]
        public BankModel BankModel { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetWithParsingSettingsAsync(id);
            if (bankDto == null)
                return NotFound();
            BankModel = bankDto.Adapt<BankModel>();
            await SetCurrencySelectListAsync(BankModel.CurrencyId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetCurrencySelectListAsync(BankModel.CurrencyId);
                return Page();
            }
            var bankUpdateDto = BankModel.Adapt<BankUpdateDto>();

            if (!await bankService.UpdateAsync(bankUpdateDto))
            {
                ValidationErrorList = bankService.ValidationService.ErrorListWithoutKeys;
                await SetCurrencySelectListAsync(BankModel.CurrencyId);
                return Page();
            }
            return RedirectToPage("./Index");
        }


    }
}

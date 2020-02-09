using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBankService bankService;
        private readonly IExchangeRateListService listService;

        public IndexModel(IBankService bank, IExchangeRateListService list)
        {
            bankService = bank;
            listService = list;
        }

        public IEnumerable<BankRateListModel> BankRateListModels { get; set; }
        public BankRateListModel FirstBankRateListModel { get; set; }

        public async Task OnGet()
        {
            var banks = await bankService.GetList();
            if (!banks.Any())
                return;
            banks = banks.OrderBy(i => i.Name);

            var lastLists = listService.GetLastExchangeRateLists();

            // Сформировать список банков с данными последних обменных курсов.
            BankRateListModels = banks.Select(bank => new BankRateListModel()
            {
                Bank = bank.Adapt<BankModel>(),
                ExchangeRateList = lastLists
                    .FirstOrDefault(i => i.BankId == bank.Id)
                    .Adapt<ExchangeRateListModel>()
            });
            FirstBankRateListModel = BankRateListModels.FirstOrDefault();
        }
    }
}

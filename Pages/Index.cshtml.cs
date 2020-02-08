using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Models;
using Mapster;

namespace RatesParsingWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBankService bankService;
        private readonly IExchangeRateListService listService;

        public IndexModel(ILogger<IndexModel> logger, IBankService bank, IExchangeRateListService list)
        {
            _logger = logger;
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

            var lists = await listService.GetAllAsync();

            // Сформировать список банков с данными последних обменных курсов.
            BankRateListModels = banks.Select(bank => new BankRateListModel()
            {
                Bank = bank.Adapt<BankModel>(),
                ExchangeRateList = lists
                    .Where(list => list.BankId == bank.Id)
                    .OrderByDescending(list => list.DateTimeStamp)
                    .FirstOrDefault()
                    .Adapt<ExchangeRateListModel>()
            });
            FirstBankRateListModel = BankRateListModels.FirstOrDefault();
        }
    }
}

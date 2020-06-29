using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Models;
using Mapster;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RatesParsingWeb.Pages.Banks
{
    public class IndexModel : PageModel
    {
        private readonly IBankService bankService;

        public IndexModel(IBankService context)
        {
            bankService = context;
        }

        public IEnumerable<BankModel> Banks { get; set; }
        public BankModel FirstBankObject { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<BankDto> bankDtos = await bankService.GetListAsync();
            if (bankDtos.Any())
            {
                Banks = bankDtos.Adapt<IEnumerable<BankModel>>();
                FirstBankObject = Banks.FirstOrDefault();          
            }
        }
    }
}

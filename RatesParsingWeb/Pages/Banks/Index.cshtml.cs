using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Models;
using Mapster;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Dto;

namespace RatesParsingWeb.Pages.Banks
{
    public class IndexModel : BaseBankPageModel
    {
        private readonly IBankService bankService;

        public IndexModel(IBankService context)
        {
            bankService = context;
        }

        public List<BankModel> BanksModelList { get; set; }
        public BankModel FirstBankObject { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<BankDto> bankDtos = await bankService.GetList();
            if (bankDtos.Any())
            {
                BanksModelList = new List<BankModel>(MapDtoToModels(bankDtos));
                FirstBankObject = BanksModelList[0];            
            }
        }
    }
}

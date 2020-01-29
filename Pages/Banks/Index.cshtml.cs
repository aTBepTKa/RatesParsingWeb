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
            IEnumerable<BankDto> banksDto = await bankService.GetAll();
            if (banksDto.Any())
            {
                BanksModelList = new List<BankModel>(banksDto.Count());
                foreach (var bank in banksDto)
                {
                    var newBankModel = bank.Adapt<BankModel>();
                    newBankModel.CurrencyModel = bank.CurrencyDto.Adapt<CurrencyModel>();
                    BanksModelList.Add(newBankModel);
                }
                FirstBankObject = BanksModelList[0];
            }
        }
    }
}

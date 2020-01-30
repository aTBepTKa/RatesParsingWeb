using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Dto;

namespace RatesParsingWeb.Pages.Banks
{
    public class DetailsModel : BaseBankPageModel
    {
        private readonly IBankService bankService;

        public DetailsModel(IBankService bank)
        {
            bankService = bank;
        }

        public BankModel BankModel { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankDto bankDto = await bankService.GetBankAsync(id);
            if (bankDto == null)
                return NotFound();
            BankModel = MapDtoToModel(bankDto);
            return Page();
        }
    }
}

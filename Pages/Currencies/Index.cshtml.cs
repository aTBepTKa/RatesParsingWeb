using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Models;
using Mapster;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Dto;

namespace RatesParsingWeb.Pages.Currencies
{
    public class IndexModel : PageModel
    {
        private readonly ICurrencyService currencyService;

        public IndexModel(ICurrencyService context)
        {
            currencyService = context;
        }

        public List<CurrencyModel> CurrencyModelList { get; set; }
        public CurrencyModel FirstCurrencyObject { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<CurrencyDto> currenciesDto = (await currencyService.GetAllAsync());
            currenciesDto = currenciesDto.OrderBy(i => i.TextCode);
            if (currenciesDto.Any())
                CurrencyModelList = new List<CurrencyModel>(currenciesDto.Adapt<IEnumerable<CurrencyModel>>());
            FirstCurrencyObject = CurrencyModelList[0];
        }
    }
}


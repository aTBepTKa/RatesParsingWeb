using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using RatesParsingWeb.Models;
using Mapster;
using RatesParsingWeb.Services.Interfaces;

namespace RatesParsingWeb.Pages.Currencies
{
    public class IndexModel : PageModel
    {
        private readonly ICurrencyService  currencyService;

        public IndexModel(ICurrencyService context)
        {
            currencyService = context;
        }

        public List<CurrencyModel> CurrencyModelList { get; set; }
        public CurrencyModel FirstCurrencyObject { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<Currency> currenciesDomain = await currencyService.GetAll();
            CurrencyModelList = currenciesDomain.Adapt<List<CurrencyModel>>();
            FirstCurrencyObject = CurrencyModelList[0];
        }
    }
}


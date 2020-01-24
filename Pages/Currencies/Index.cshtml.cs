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

namespace RatesParsingWeb.Pages.Currencies
{
    public class IndexModel : PageModel
    {
        private readonly ICurrencyRepository currencyRepository;

        public IndexModel(ICurrencyRepository context)
        {
            currencyRepository = context;
        }

        public List<CurrencyModel> CurrencyModelList { get; set; }
        public CurrencyModel FirstCurrencyObject { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<Currency> currenciesDomain = await currencyRepository.GetAll();
            CurrencyModelList = currenciesDomain.Adapt<List<CurrencyModel>>();
            FirstCurrencyObject = CurrencyModelList[0];
        }
    }
}


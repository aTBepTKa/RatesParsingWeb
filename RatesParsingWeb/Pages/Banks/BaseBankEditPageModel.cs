using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Models;
using RatesParsingWeb.Models.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Pages.Banks
{
    /// <summary>
    /// Средства для обработки данных банков.
    /// </summary>
    public class BaseBankEditPageModel : PageModel
    {
        private readonly ICurrencyService currencyService;
        private readonly ICommandService commandService;

        /// <summary>
        /// Выпадающий список для выбора валюты.
        /// </summary>
        public SelectList CurrencySelectList { get; set; }

        /// <summary>
        /// Выпадающий список для выбора команды.
        /// </summary>
        public SelectList CommandSelectList { get; set; }

        /// <summary>
        /// Список ошибок валидации.
        /// </summary>
        public IEnumerable<string> ValidationErrorList { get; set; }

        public BaseBankEditPageModel(ICurrencyService currencyService, ICommandService commandService)
        {
            this.currencyService = currencyService;
            this.commandService = commandService;
        }

        /// <summary>
        /// Задать выпадающий список для выбора валюты.
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        protected async Task SetCurrencySelectListAsync(int? selectedId = null)
        {
            var currenciesDto = (await currencyService.GetAllAsync()).OrderBy(i => i.TextCode);
            var currenciesSelectList = currenciesDto.Adapt<IEnumerable<CurrencySelectListModel>>();

            CurrencySelectList = new SelectList(currenciesSelectList,
                                            nameof(CurrencySelectListModel.Id),
                                            nameof(CurrencySelectListModel.CodeWithName),
                                            currenciesSelectList.FirstOrDefault(i => i.Id == selectedId));
        }
    }
}

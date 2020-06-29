using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Models;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Dto;
using Mapster;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using RatesParsingWeb.Models.ParsingSettings;

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
        /// Задать выпадающий список выбора валют и команд.
        /// </summary>
        /// <param name="currencyId"></param>
        /// <param name="commandId"></param>
        /// <returns></returns>
        protected async Task SetSeletListsAsync(int? currencyId = null, int? commandId = null)
        {
            await SetCurrencySelectListAsync(currencyId);
            await SetCommandSelectListAsync(commandId);
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

        /// <summary>
        /// Задать выпадающий список для выбора команды.
        /// </summary>
        /// <param name="selectedCommand"></param>
        /// <returns></returns>
        protected async Task SetCommandSelectListAsync(int? selectedId = null)
        {
            var commands = await GetExternalCommandsList();
            CommandSelectList = new SelectList(commands,
                                               nameof(CommandModel.Name),
                                               $"{nameof(CommandModel.Name)} - {nameof(CommandModel.Description)}",
                                               commands.FirstOrDefault(i=>i.Id == selectedId));
        }

        private async Task<IEnumerable<CommandModel>> GetExternalCommandsList()
        {
            var commands = await commandService.GetExternalCommands();
            if (commands.IsSuccesfullParsed)
                return commands.Message.Adapt<IEnumerable<CommandModel>>();
            else
                return Array.Empty<CommandModel>();
        }
    }
}

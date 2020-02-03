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

namespace RatesParsingWeb.Pages.Banks
{
    /// <summary>
    /// Средства для обработки данных банков.
    /// </summary>
    public class BaseBankPageModel : PageModel
    {
        /// <summary>
        /// Выпадающий список для выбора валюты.
        /// </summary>
        public SelectList CurrencySelectList { get; set; }

        /// <summary>
        /// Список ошибок валидации.
        /// </summary>
        public IValidationDictionary ValidationDictionary { get; set; }

        /// <summary>
        /// Задать данные для выпадающего списка.
        /// </summary>
        /// <param name="SelectedId"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        protected async Task SetCurrencySelectListAsync(int? SelectedId, ICurrencyService currency)
        {
            var currenciesDto = (await currency.GetAllAsync()).OrderBy(i => i.TextCode);
            var currenciesSelectList = currenciesDto.Adapt<IEnumerable<CurrencySelectListModel>>();

            CurrencySelectList = new SelectList(currenciesSelectList,
                                            nameof(CurrencySelectListModel.Id),
                                            nameof(CurrencySelectListModel.CodeWithName),
                                            currenciesSelectList.FirstOrDefault(i => i.Id == SelectedId));
        }

        /// <summary>
        /// Преобразовать модель DTO в модель представления.
        /// </summary>
        /// <param name="bankDto"></param>
        /// <returns></returns>
        protected BankModel MapDtoToModel(BankDto bankDto) =>
            bankDto?.Adapt<BankModel>();

        /// <summary>
        /// Преобразовать коллекцию моделей DTO в модели представления.
        /// </summary>
        /// <param name="bankDtos"></param>
        /// <returns></returns>
        protected IEnumerable<BankModel> MapDtoToModels(IEnumerable<BankDto> bankDtos) =>
            bankDtos?.Adapt<IEnumerable<BankModel>>();

        /// <summary>
        /// Преобразовать модель представления в модель DTO.
        /// </summary>
        /// <param name="bankModel"></param>
        /// <returns></returns>
        protected BankDto MapModelToDto(BankModel bankModel) =>
            bankModel?.Adapt<BankDto>();
    }
}

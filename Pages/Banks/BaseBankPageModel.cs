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

namespace RatesParsingWeb.Pages.Banks
{
    /// <summary>
    /// Средства для обработки данных банков.
    /// </summary>
    public class BaseBankPageModel : PageModel
    {
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

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
        protected BankModel MapDtoToModel(BankDto bankDto)
        {
            if (bankDto != null)
            {
                BankModel bankModel = bankDto.Adapt<BankModel>();
                if (bankDto.CurrencyDto != null)
                    bankModel.CurrencyModel = bankDto.CurrencyDto.Adapt<CurrencyModel>();
                if (bankDto.ParsingSettingsDto != null)
                    bankModel.ParsingSettingsModel = bankDto.ParsingSettingsDto.Adapt<ParsingSettingsModel>();
                return bankModel;
            }
            else
                return null;
        }

        /// <summary>
        /// Преобразовать коллекцию моделей DTO в модели представления.
        /// </summary>
        /// <param name="bankDtos"></param>
        /// <returns></returns>
        protected IEnumerable<BankModel> MapDtoToModels(IEnumerable<BankDto> bankDtos)
        {            
            if (bankDtos.Any())
            {
                var bankModels = new List<BankModel>(bankDtos.Count());
                foreach (var dto in bankDtos)
                    bankModels.Add(MapDtoToModel(dto));
                return bankModels;
            }
            else
                return Enumerable.Empty<BankModel>();
        }

        /// <summary>
        /// Преобразовать модель представления в модель DTO.
        /// </summary>
        /// <param name="bankModel"></param>
        /// <returns></returns>
        protected BankDto MapModelToDto(BankModel bankModel)
        {
            if (bankModel != null)
            {
                BankDto bankDto = bankModel.Adapt<BankDto>();
                if (bankModel.CurrencyModel != null)
                {
                    bankDto.CurrencyId = bankModel.CurrencyModel.Id;
                    bankDto.CurrencyDto = bankModel.CurrencyModel.Adapt<CurrencyDto>();
                }
                if (bankModel.ParsingSettingsModel != null)
                {
                    bankDto.ParsingSettingsId = bankModel.ParsingSettingsModel.Id;
                    bankDto.ParsingSettingsDto = bankModel.ParsingSettingsModel.Adapt<ParsingSettingsDto>();
                }
                
                return bankDto;
            }
            else
                return null;
        }
    }
}

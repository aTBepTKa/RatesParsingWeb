using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Storage.Repositories;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class BankService : BaseCrudService<BankDto, Bank>, IBankService
    {
        private IBankRepository BankRepository { get; set; }
        public BankService(BankRatesContext context)
        {
            BankRepository = new BankRepository(context);
            BaseRepository = BankRepository;
        }

        public override bool IsValid(BankDto bank)
        {
            // Пускай побудет, придумать как передавать на верхний слой. 
            ModelStateDictionary modelState = new ModelStateDictionary();

            // Проверить SwiftCode.
            if (string.IsNullOrEmpty(bank.SwiftCode))
                modelState.AddModelError("SwiftRequired", "SWIFT код обязателен.");
            if (bank.SwiftCode.Length != 11)
                modelState.AddModelError("SwiftLength", "Длина SWIF кода составляет 11 символов.");

            // Проверить Name.
            if (string.IsNullOrEmpty(bank.Name))
                modelState.AddModelError("NameRequired", "Название банка обязательно.");
            if (bank.Name.Length > 50)
                modelState.AddModelError("NameLength", "Максимальная длина названия банка не более 50 символов.");

            // Проверить BankUrl.
            if (!Uri.TryCreate(bank.BankUrl, UriKind.Absolute, out _))
                modelState.AddModelError("BankUrl", "Ссылка страницы банка некорректна.");

            // Проверить RatesUrl.
            if (string.IsNullOrEmpty(bank.Name))
                modelState.AddModelError("NameRequired", "Страница курсов обязательна.");
            if (!Uri.TryCreate(bank.RatesUrl, UriKind.Absolute, out _))
                modelState.AddModelError("RatesUrl", "Ссылка страницы курсов банка некорректна.");

            // Проверить CurrencyId.
            if (bank.CurrencyDto.Id == 0)
                modelState.AddModelError("CurrencyTextCode", "Основная валюта банка отсутствует в базе.");
            return modelState.IsValid;
        }

        public async Task<IEnumerable<BankDto>> GetBankListAsync()
        {
            var banks = await BankRepository.GetAllAsync(i => i.Currency);
            var bankDtoList = new List<BankDto>(banks.Count());
            foreach (var bank in banks)
                bankDtoList.Add(MapDomainToDto(bank));
            return bankDtoList;
        }

        public async Task<BankDto> GetBankAsync(int id)
        {
            var bankDomain = await BankRepository.GetByIdAsync(id,
                c => c.Currency,
                s => s.ParsingSettings);

            var bankDto = MapDomainToDto(bankDomain);
            return bankDto;
        }

        public async Task<bool> UpdateBankAsync(BankDto bankDto)
        {
            if (!IsValid(bankDto))
                return false;
            Bank bankToUpdate = await BankRepository.GetByIdAsync(bankDto.Id);
            if (bankToUpdate == null)
                return false;

            bankDto.Adapt(bankToUpdate);
            BankRepository.SetStateModifed(bankToUpdate);
            await BankRepository.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Преобразовать доменную модель в модель DTO.
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        private BankDto MapDomainToDto(Bank bank)
        {
            if (bank != null)
            {
                var newBankDto = bank.Adapt<BankDto>();
                if (bank.Currency != null)
                    newBankDto.CurrencyDto = bank.Currency.Adapt<CurrencyDto>();
                if (bank.ParsingSettings != null)
                    newBankDto.ParsingSettingsDto = bank.ParsingSettings.Adapt<ParsingSettingsDto>();
                return newBankDto;
            }
            else
                return null;
        }

        /// <summary>
        /// Преобразовать модель DTO в доменную модель.
        /// </summary>
        /// <param name="bankDto"></param>
        /// <returns></returns>
        private Bank MapDtoToDomain(BankDto bankDto)
        {
            if (bankDto != null)
            {
                var newBankDomain = bankDto.Adapt<Bank>();
                if (bankDto.CurrencyDto != null)
                    newBankDomain.Currency = bankDto.CurrencyDto.Adapt<Currency>();
                if (bankDto.ParsingSettingsDto != null)
                    newBankDomain.ParsingSettings = bankDto.ParsingSettingsDto.Adapt<ParsingSettings>();
                return newBankDomain;
            }
            else
                return null;
        }
    }
}

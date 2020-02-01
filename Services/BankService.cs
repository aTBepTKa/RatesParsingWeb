using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.UpdateAndCreate;
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
        private readonly IBankRepository bankRepository;
        public BankService(IBankRepository repository) : base(repository)
        {
            bankRepository = repository;
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

            return modelState.IsValid;
        }

        public async Task<IEnumerable<BankDto>> GetList()
        {
            var banks = await bankRepository.GetAllAsync(i => i.Currency);
            return banks.Adapt<IEnumerable<BankDto>>();
        }

        public async Task<BankDto> GetById(int id)
        {
            var bankDomain = await bankRepository.GetSingleAsync(
                i => i.Id == id,
                c => c.Currency,
                s => s.ParsingSettings);
            return bankDomain.Adapt<BankDto>();
        }

        public async Task<bool> UpdateBankAsync(BankUpdateDto updateDto)
        {
            Bank bankToUpdate = await bankRepository.FindAsync(updateDto.Id);

            if (bankToUpdate == null)
                throw new Exception($"Банк с Id '{updateDto.Id}' не найден.");
            if (!IsValid(updateDto.Adapt<BankDto>()))
                return false;

            updateDto.Adapt(bankToUpdate);
            bankRepository.SetStateModifed(bankToUpdate);
            await bankRepository.SaveChangesAsync();
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
                    newBankDto.Currency = bank.Currency.Adapt<CurrencyDto>();
                if (bank.ParsingSettings != null)
                    newBankDto.ParsingSettings = bank.ParsingSettings.Adapt<ParsingSettingsDto>();
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
                if (bankDto.Currency != null)
                    newBankDomain.Currency = bankDto.Currency.Adapt<Currency>();
                if (bankDto.ParsingSettings != null)
                    newBankDomain.ParsingSettings = bankDto.ParsingSettings.Adapt<ParsingSettings>();
                return newBankDomain;
            }
            else
                return null;
        }
    }
}

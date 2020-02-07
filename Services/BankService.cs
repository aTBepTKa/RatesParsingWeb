  using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RatesParsingWeb.Extentions;

namespace RatesParsingWeb.Services
{
    public class BankService : BaseCrudService<BankDto, Bank>, IBankService
    {
        private readonly IBankRepository bankRepository;
        private readonly IParsingSettingsRepository parsingSettingsRepository;
        public BankService(IBankRepository bank, IParsingSettingsRepository parsing) : base(bank)
        {
            bankRepository = bank;
            parsingSettingsRepository = parsing;
        }

        public async Task<IEnumerable<BankDto>> GetList()
        {
            var banks = await bankRepository.GetAllAsync(i => i.Currency);
            return banks.Adapt<IEnumerable<BankDto>>();
        }

        public async Task<BankDto> GetByIdAsync(int id)
        {         
            var bankDomain = await bankRepository.GetSingleAsync(
                i => i.Id == id,
                c => c.Currency,
                s => s.ParsingSettings);
            return bankDomain.Adapt<BankDto>();
        }

        public async Task<bool> CreateAsync(BankCreateDto createDto)
        {
            // TODO: Добавить валидацию настроек парсинга.
            await CheckCreateForUniqueness(createDto);
            CheckForValidity(createDto);
            if (!ValidationDictionary.IsValid)
                return false;

            var bankToCreate = createDto.Adapt<Bank>();
            await bankRepository.AddAsync(bankToCreate);
            await bankRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(BankUpdateDto updateDto)
        {
            await CheckUpdateForUniqueness(updateDto);
            CheckForValidity(updateDto);
            if (!ValidationDictionary.IsValid)
                return false;

            Bank bankToUpdate = await bankRepository.GetFirstOrDefaultAsync(
                i => i.Id == updateDto.Id,
                i => i.ParsingSettings);

            if (bankToUpdate == null)
                throw new Exception($"Банк с Id '{bankToUpdate.Id}' не найден.");

            updateDto.Adapt(bankToUpdate);
            bankRepository.SetStateModifed(bankToUpdate);
            await bankRepository.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var bankToDelete = await bankRepository.FindAsync(id);
            if (bankToDelete == null)
                throw new Exception($"Банк с Id '{bankToDelete.Id}' не найден.");
            bankRepository.Remove(bankToDelete);
            await bankRepository.SaveChangesAsync();
        }

        private void CheckForValidity(IBankRequisites bank)
        {
            // Проверить основные свойства банка.

            // SwiftCode.
            if (string.IsNullOrEmpty(bank.SwiftCode))
                ValidationDictionary.AddError(nameof(bank.SwiftCode), "SWIFT код обязателен.");
            else if (bank.SwiftCode.Length != 11)
                ValidationDictionary.AddError(nameof(bank.SwiftCode), "Длина SWIFT кода составляет 11 символов.");

            // Name.
            if (string.IsNullOrEmpty(bank.Name))
                ValidationDictionary.AddError(nameof(bank.Name), "Название банка обязательно.");
            else if (bank.Name.Length > 50)
                ValidationDictionary.AddError(nameof(bank.Name), "Максимальная длина названия банка не более 50 символов.");

            // BankUrl.
            if (!string.IsNullOrEmpty(bank.BankUrl) && !Uri.TryCreate(bank.BankUrl, UriKind.Absolute, out _))
                ValidationDictionary.AddError(nameof(bank.BankUrl), "Ссылка на страницу банка некорректна.");

            // RatesUrl.
            if (string.IsNullOrEmpty(bank.RatesUrl))
                ValidationDictionary.AddError(nameof(bank.RatesUrl), "Страница курсов обязательна.");
            else if (!Uri.TryCreate(bank.RatesUrl, UriKind.Absolute, out _))
                ValidationDictionary.AddError(nameof(bank.RatesUrl), "Ссылка на страницу курсов банка некорректна.");


            // Проверить настройки парсинга.

            // TextCodeXPath.
            if (string.IsNullOrEmpty(bank.ParsingSettings.TextCodeXpath))
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.TextCodeXpath), "XPath для текстового кода обязателен.");
            else if (bank.ParsingSettings.TextCodeXpath.Length > 2000)
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.TextCodeXpath), "Максимальная длина XPath для текстового кода составляет 2000 символов.");

            // UnitXPath.
            if (string.IsNullOrEmpty(bank.ParsingSettings.UnitXpath))
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.UnitXpath), "XPath для единицы измерения обязателен.");
            else if (bank.ParsingSettings.UnitXpath.Length > 2000)
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.UnitXpath), "Максимальная длина XPath для единицы измерения составляет 2000 символов.");

            // ExchangeRateXpath.
            if (string.IsNullOrEmpty(bank.ParsingSettings.ExchangeRateXpath))
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.ExchangeRateXpath), "XPath для обменного курса обязателен.");
            else if (bank.ParsingSettings.ExchangeRateXpath.Length > 2000)
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.ExchangeRateXpath), "Максимальная длина XPath для обменного курса составляет 2000 символов.");

            // ExchangeRateXpath.
            if (string.IsNullOrEmpty(bank.ParsingSettings.VariablePartOfXpath))
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.VariablePartOfXpath), "Изменяемая часть XPath обязателена.");
            else if (bank.ParsingSettings.VariablePartOfXpath.Length > 50)
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.VariablePartOfXpath), "Максимальная длина изменяемой части XPath составляет 50 символов.");

            // StartXpathRow.
            if (bank.ParsingSettings.StartXpathRow < 1)
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.VariablePartOfXpath), "Номер первой строки для парсинга должен быть не менее единицы.");

            // EndXpathRow.
            if (bank.ParsingSettings.EndXpathRow < bank.ParsingSettings.StartXpathRow)
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.VariablePartOfXpath), "Номер последней строки для парсинга должен быть больше номера первой строки.");

            // NumberDecimalSeparator.
            if (string.IsNullOrEmpty(bank.ParsingSettings.NumberDecimalSeparator))
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.NumberDecimalSeparator), "Разделитель десятичной части обязателен.");
            else if (bank.ParsingSettings.NumberDecimalSeparator.Length != 1)
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.NumberDecimalSeparator), "Разделитель десятичной части должен быть представлен одним символом.");

            // NumberGroupSeparator.
            if (!string.IsNullOrEmpty(bank.ParsingSettings.NumberGroupSeparator) &&
                bank.ParsingSettings.NumberGroupSeparator.Length != 1)
                ValidationDictionary.AddError(nameof(bank.ParsingSettings.NumberGroupSeparator), "Разделитель десятичной части должен быть представлен одним символом.");
        }

        private async Task CheckUpdateForUniqueness(BankUpdateDto bankUpdate)
        {
            if (await bankRepository.AnyAsync(i => i.Id != bankUpdate.Id && i.Name == bankUpdate.Name))
            {
                ValidationDictionary.AddError(nameof(bankUpdate.Name), $"Банк с именем '{bankUpdate.Name}' уже существует.");
            }


            if (await bankRepository.AnyAsync(i => i.Id != bankUpdate.Id && i.SwiftCode == bankUpdate.SwiftCode))
            {
                ValidationDictionary.AddError(nameof(bankUpdate.SwiftCode), $"Банк со SWIFT кодом '{bankUpdate.SwiftCode}' уже существует.");
            }
        }

        private async Task CheckCreateForUniqueness(BankCreateDto bankCreate)
        {
            if (await bankRepository.AnyAsync(i => i.Name == bankCreate.Name))
                ValidationDictionary.AddError(nameof(bankCreate.Name), $"Банк с именем '{bankCreate.Name}' уже существует.");

            if (await bankRepository.AnyAsync(i => i.SwiftCode == bankCreate.SwiftCode))
                ValidationDictionary.AddError(nameof(bankCreate.SwiftCode), $"Банк со SWIFT кодом '{bankCreate.SwiftCode}' уже существует.");
        }
    }
}

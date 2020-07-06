using Mapster;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.Bank;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class BankService : BaseCrudService<BankDto, Bank>, IBankService
    {
        private readonly IBankRepository bankRepository;

        /// <summary>
        /// Максимальное количество символов в ссылке.
        /// </summary>
        private int UriMaxLength { get; } = 2000;

        /// <summary>
        /// Максимальное количество символов в изменяемой части Xpath.
        /// </summary>
        private int VariablePartOfXpathMaxLength { get; } = 50;

        public BankService(IBankRepository bankRepository) : base(bankRepository)
        {
            this.bankRepository = bankRepository;
        }

        public async Task<IEnumerable<BankDto>> GetListAsync()
        {
            var banks = await bankRepository.GetAllAsync(i => i.Currency);
            return banks.Adapt<IEnumerable<BankDto>>();
        }

        #region GetBank
        public async Task<BankDto> GetWithParsingSettingsAsync(int id)
        {
            var bankSettings = await bankRepository.GetWithSettings(id);
            var bankDto = bankSettings.Adapt<BankDto>();
            return bankDto;
        }

        public async Task<BankDto> GetWithParsingSettingsAsync(string swiftCode)
        {
            var bank = await bankRepository.GetFirstOrDefaultAsync(b => b.SwiftCode == swiftCode);

            if (bank is null)
                throw new ArgumentNullException(nameof(swiftCode), $"Банк со SWIFT кодом '{swiftCode}' не найден.");

            var bankSettings = await bankRepository.GetWithSettings(bank.Id);
            var bankDto = bankSettings.Adapt<BankDto>();
            return bankDto;
        }

        public async Task<BankDto> GetWithCurrencyAsync(int id)
        {
            var bankDto = (await bankRepository.GetFirstOrDefaultAsync(
                i => i.Id == id,
                i => i.Currency))
            .Adapt<BankDto>();

            if (bankDto is null)
                throw new ArgumentNullException(nameof(id), $"Банк с Id '{id}' не найден.");

            return bankDto;
        }
        #endregion


        public async Task<bool> CreateAsync(BankCreateDto createDto)
        {
            await CheckCreateForUniqueness(createDto);
            CheckForValidity(createDto);
            if (!ValidationService.IsValid)
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
            if (!ValidationService.IsValid)
                return false;

            Bank bankToUpdate = await bankRepository.GetFirstOrDefaultAsync(
                i => i.Id == updateDto.Id,
                i => i.ParsingSettings);

            if (bankToUpdate is null)
                throw new ArgumentNullException(nameof(bankToUpdate.Id), $"Банк с Id '{bankToUpdate.Id}' не найден.");

            updateDto.Adapt(bankToUpdate);
            bankRepository.SetStateModifed(bankToUpdate);
            await bankRepository.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var bankToDelete = await bankRepository.FindAsync(id);
            if (bankToDelete is null)
                throw new ArgumentNullException(nameof(id),$"Банк с Id '{bankToDelete.Id}' не найден.");
            bankRepository.Remove(bankToDelete);
            await bankRepository.SaveChangesAsync();
        }

        private void CheckForValidity(IBankValidity bank)
        {
            // Проверить основные свойства банка.

            // SwiftCode.
            if (string.IsNullOrWhiteSpace(bank.SwiftCode))
                ValidationService.AddError(nameof(bank.SwiftCode), "SWIFT код обязателен.");
            else if (bank.SwiftCode.Length != 11)
                ValidationService.AddError(nameof(bank.SwiftCode), "Длина SWIFT кода составляет 11 символов.");

            // Name.
            if (string.IsNullOrWhiteSpace(bank.Name))
                ValidationService.AddError(nameof(bank.Name), "Название банка обязательно.");
            else if (bank.Name.Length > 50)
                ValidationService.AddError(nameof(bank.Name), "Максимальная длина названия банка не более 50 символов.");

            // BankUrl.
            if (!string.IsNullOrWhiteSpace(bank.BankUrl) && !Uri.TryCreate(bank.BankUrl, UriKind.Absolute, out _))
                ValidationService.AddError(nameof(bank.BankUrl), "Ссылка на страницу банка некорректна.");

            // RatesUrl.
            if (string.IsNullOrWhiteSpace(bank.RatesUrl))
                ValidationService.AddError(nameof(bank.RatesUrl), "Страница курсов обязательна.");
            else if (!Uri.TryCreate(bank.RatesUrl, UriKind.Absolute, out _))
                ValidationService.AddError(nameof(bank.RatesUrl), "Ссылка на страницу курсов банка некорректна.");


            // Проверить настройки парсинга.
            // RatesUrl
            if (!string.IsNullOrWhiteSpace(bank.ParsingSettings.RatesUrl) &&
                !Uri.TryCreate(bank.ParsingSettings.RatesUrl, UriKind.Absolute, out _))
                ValidationService.AddError(nameof(bank.ParsingSettings.RatesUrl), "Ссылка на страницу банка некорректна.");

            // TextCodeXPath.
            if (string.IsNullOrWhiteSpace(bank.ParsingSettings.TextCodeXpath))
                ValidationService.AddError(nameof(bank.ParsingSettings.TextCodeXpath), "XPath для текстового кода обязателен.");
            else if (bank.ParsingSettings.TextCodeXpath.Length > UriMaxLength)
                ValidationService.AddError(nameof(bank.ParsingSettings.TextCodeXpath), $"Максимальная длина XPath для текстового кода составляет {UriMaxLength} символов.");

            // UnitXPath.
            if (string.IsNullOrWhiteSpace(bank.ParsingSettings.UnitXpath))
                ValidationService.AddError(nameof(bank.ParsingSettings.UnitXpath), "XPath для единицы измерения обязателен.");
            else if (bank.ParsingSettings.UnitXpath.Length > UriMaxLength)
                ValidationService.AddError(nameof(bank.ParsingSettings.UnitXpath), $"Максимальная длина XPath для единицы измерения составляет {UriMaxLength} символов.");

            // ExchangeRateXpath.
            if (string.IsNullOrWhiteSpace(bank.ParsingSettings.ExchangeRateXpath))
                ValidationService.AddError(nameof(bank.ParsingSettings.ExchangeRateXpath), "XPath для обменного курса обязателен.");
            else if (bank.ParsingSettings.ExchangeRateXpath.Length > UriMaxLength)
                ValidationService.AddError(nameof(bank.ParsingSettings.ExchangeRateXpath), $"Максимальная длина XPath для обменного курса составляет {UriMaxLength} символов.");

            // ExchangeRateXpath.
            if (string.IsNullOrWhiteSpace(bank.ParsingSettings.VariablePartOfXpath))
                ValidationService.AddError(nameof(bank.ParsingSettings.VariablePartOfXpath), "Изменяемая часть XPath обязателена.");
            else if (bank.ParsingSettings.VariablePartOfXpath.Length > VariablePartOfXpathMaxLength)
                ValidationService.AddError(nameof(bank.ParsingSettings.VariablePartOfXpath), $"Максимальная длина изменяемой части XPath составляет {VariablePartOfXpathMaxLength} символов.");

            // StartXpathRow.
            if (bank.ParsingSettings.StartXpathRow < 1)
                ValidationService.AddError(nameof(bank.ParsingSettings.VariablePartOfXpath), "Номер первой строки для парсинга должен быть не менее единицы.");

            // EndXpathRow.
            if (bank.ParsingSettings.EndXpathRow < bank.ParsingSettings.StartXpathRow)
                ValidationService.AddError(nameof(bank.ParsingSettings.VariablePartOfXpath), "Номер последней строки для парсинга должен быть больше номера первой строки.");

            // NumberDecimalSeparator.
            if (string.IsNullOrEmpty(bank.ParsingSettings.NumberDecimalSeparator))
                ValidationService.AddError(nameof(bank.ParsingSettings.NumberDecimalSeparator), "Разделитель десятичной части обязателен.");
            else if (bank.ParsingSettings.NumberDecimalSeparator.Length != 1)
                ValidationService.AddError(nameof(bank.ParsingSettings.NumberDecimalSeparator), "Разделитель десятичной части должен быть представлен одним символом.");

            // NumberGroupSeparator.
            if (!string.IsNullOrEmpty(bank.ParsingSettings.NumberGroupSeparator) &&
                bank.ParsingSettings.NumberGroupSeparator.Length != 1)
                ValidationService.AddError(nameof(bank.ParsingSettings.NumberGroupSeparator), "Разделитель десятичной части должен быть представлен одним символом.");
        }

        private async Task CheckUpdateForUniqueness(BankUpdateDto bankUpdate)
        {
            if (await bankRepository.AnyAsync(i => i.Id != bankUpdate.Id && i.Name == bankUpdate.Name))
                ValidationService.AddError(nameof(bankUpdate.Name), $"Банк с именем '{bankUpdate.Name}' уже существует.");

            if (await bankRepository.AnyAsync(i => i.Id != bankUpdate.Id && i.SwiftCode == bankUpdate.SwiftCode))
                ValidationService.AddError(nameof(bankUpdate.SwiftCode), $"Банк со SWIFT кодом '{bankUpdate.SwiftCode}' уже существует.");

        }

        private async Task CheckCreateForUniqueness(BankCreateDto bankCreate)
        {
            if (await bankRepository.AnyAsync(i => i.Name == bankCreate.Name))
                ValidationService.AddError(nameof(bankCreate.Name), $"Банк с именем '{bankCreate.Name}' уже существует.");

            if (await bankRepository.AnyAsync(i => i.SwiftCode == bankCreate.SwiftCode))
                ValidationService.AddError(nameof(bankCreate.SwiftCode), $"Банк со SWIFT кодом '{bankCreate.SwiftCode}' уже существует.");
        }
    }
}

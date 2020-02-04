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

        public async Task<BankDto> GetById(int id)
        {
            var bankDomain = await bankRepository.GetSingleAsync(
                i => i.Id == id,
                c => c.Currency,
                s => s.ParsingSettings);
            return bankDomain.Adapt<BankDto>();
        }

        public async Task<bool> CreateBankAsync(BankCreateDto createDto)
        {
            // TODO: Добавить валидацию настроек парсинга.
            CheckForValidity(createDto);
            await CheckCreateForUniqueness(createDto);
            if (!ValidationDictionary.IsValid)
                return false;

            var bankToCreate = createDto.Adapt<Bank>();
            await bankRepository.AddAsync(bankToCreate);
            await bankRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBankAsync(BankUpdateDto updateDto)
        {
            CheckForValidity(updateDto);
            await CheckUpdateForUniqueness(updateDto);
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

        private void CheckForValidity(IBankRequisites bank)
        {
            // Проверить основные свойства банка.
            // SwiftCode.
            if (string.IsNullOrEmpty(bank.SwiftCode))
                ValidationDictionary.AddError("SwiftRequired", "SWIFT код обязателен.");
            else if (bank.SwiftCode.Length != 11)
                ValidationDictionary.AddError("SwiftLength", "Длина SWIFT кода составляет 11 символов.");

            // Name.
            if (string.IsNullOrEmpty(bank.Name))
                ValidationDictionary.AddError("NameRequired", "Название банка обязательно.");
            else if (bank.Name.Length > 50)
                ValidationDictionary.AddError("NameLength", "Максимальная длина названия банка не более 50 символов.");

            // BankUrl.
            if (!string.IsNullOrEmpty(bank.BankUrl) && !Uri.TryCreate(bank.BankUrl, UriKind.Absolute, out _))
                ValidationDictionary.AddError("BankUrl", "Ссылка страницы банка некорректна.");

            // RatesUrl.
            if (string.IsNullOrEmpty(bank.RatesUrl))
                ValidationDictionary.AddError("NameRequired", "Страница курсов обязательна.");
            else if (!Uri.TryCreate(bank.RatesUrl, UriKind.Absolute, out _))
                ValidationDictionary.AddError("RatesUrl", "Ссылка страницы курсов банка некорректна.");


            // Проверить настройки парсинга.

        }

        private async Task CheckUpdateForUniqueness(BankUpdateDto bankUpdate)
        {            
            Expression<Func<Bank, bool>> nameExpession = i => 
                i.Id != bankUpdate.Id &&
                i.Name == bankUpdate.Name;
            if (await bankRepository.AnyAsync(nameExpession))
                ValidationDictionary.AddError("Name", $"Банк с именем '{bankUpdate.Name}' уже существует.");

            Expression<Func<Bank, bool>> swiftExpression = i =>
                i.Id != bankUpdate.Id &&
                i.SwiftCode == bankUpdate.SwiftCode;
            if (await bankRepository.AnyAsync(swiftExpression))
                ValidationDictionary.AddError("Name", $"Банк со SWIFT кодом '{bankUpdate.SwiftCode}' уже существует.");
        }

        private async Task CheckCreateForUniqueness(BankCreateDto bankCreate)
        {
            Expression<Func<Bank, bool>> nameExpession = i =>
                i.Name == bankCreate.Name;
            if (await bankRepository.AnyAsync(nameExpession))
                ValidationDictionary.AddError("Name", $"Банк с именем '{bankCreate.Name}' уже существует.");

            Expression<Func<Bank, bool>> swiftExpression = i =>
                i.SwiftCode == bankCreate.SwiftCode;
            if (await bankRepository.AnyAsync(swiftExpression))
                ValidationDictionary.AddError("Name", $"Банк со SWIFT кодом '{bankCreate.SwiftCode}' уже существует.");
        }
    }
}

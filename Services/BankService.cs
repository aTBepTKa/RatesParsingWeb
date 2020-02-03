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
        public BankService(IBankRepository repository) : base(repository)
        {
            bankRepository = repository;
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
            ValidationCheck(updateDto);
            await UniqueCheck(updateDto, i => i.Id != updateDto.Id);
            if (!ValidationDictionary.IsValid)
                return false;

            Bank bankToUpdate = await bankRepository.GetSingleAsync(
                i => i.Id == updateDto.Id,
                i => i.ParsingSettings);

            updateDto.Adapt(bankToUpdate);
            bankRepository.SetStateModifed(bankToUpdate);
            await bankRepository.SaveChangesAsync();
            return true;
        }

        private void ValidationCheck(IBankRequisites bank)
        {
            // Проверить SwiftCode.
            if (string.IsNullOrEmpty(bank.SwiftCode))
                ValidationDictionary.AddError("SwiftRequired", "SWIFT код обязателен.");
            else if (bank.SwiftCode.Length != 11)
                ValidationDictionary.AddError("SwiftLength", "Длина SWIFT кода составляет 11 символов.");

            // Проверить Name.
            if (string.IsNullOrEmpty(bank.Name))
                ValidationDictionary.AddError("NameRequired", "Название банка обязательно.");
            else if (bank.Name.Length > 50)
                ValidationDictionary.AddError("NameLength", "Максимальная длина названия банка не более 50 символов.");

            // Проверить BankUrl.
            if (!Uri.TryCreate(bank.BankUrl, UriKind.Absolute, out _))
                ValidationDictionary.AddError("BankUrl", "Ссылка страницы банка некорректна.");

            // Проверить RatesUrl.
            if (string.IsNullOrEmpty(bank.RatesUrl))
                ValidationDictionary.AddError("NameRequired", "Страница курсов обязательна.");
            else if (!Uri.TryCreate(bank.RatesUrl, UriKind.Absolute, out _))
                ValidationDictionary.AddError("RatesUrl", "Ссылка страницы курсов банка некорректна.");
        }

        private async Task UniqueCheck(IBankRequisites bank, Expression<Func<Bank, bool>> where)
        {
            // Объединить выражение where с выражением отбора по имени.
            Expression<Func<Bank, bool>> nameExpession = i => i.Name == bank.Name;
            if (await bankRepository.AnyWhereAsync(where, nameExpession))
                ValidationDictionary.AddError("Name", $"Банк с именем '{bank.Name}' уже существует.");

            Expression<Func<Bank, bool>> swiftExpression = i => i.SwiftCode == bank.SwiftCode;
            if (await bankRepository.AnyWhereAsync(where, swiftExpression))
                ValidationDictionary.AddError("Name", $"Банк со SWIFT кодом '{bank.SwiftCode}' уже существует.");
        }
    }
}

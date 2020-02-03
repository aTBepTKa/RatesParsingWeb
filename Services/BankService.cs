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
            if (!ModelState.IsValid)
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
                ModelState.AddError("SwiftRequired", "SWIFT код обязателен.");
            else if (bank.SwiftCode.Length != 11)
                ModelState.AddError("SwiftLength", "Длина SWIFT кода составляет 11 символов.");

            // Проверить Name.
            if (string.IsNullOrEmpty(bank.Name))
                ModelState.AddError("NameRequired", "Название банка обязательно.");
            else if (bank.Name.Length > 50)
                ModelState.AddError("NameLength", "Максимальная длина названия банка не более 50 символов.");

            // Проверить BankUrl.
            if (!Uri.TryCreate(bank.BankUrl, UriKind.Absolute, out _))
                ModelState.AddError("BankUrl", "Ссылка страницы банка некорректна.");

            // Проверить RatesUrl.
            if (string.IsNullOrEmpty(bank.RatesUrl))
                ModelState.AddError("NameRequired", "Страница курсов обязательна.");
            else if (!Uri.TryCreate(bank.RatesUrl, UriKind.Absolute, out _))
                ModelState.AddError("RatesUrl", "Ссылка страницы курсов банка некорректна.");
        }

        private async Task UniqueCheck(IBankRequisites bank, Expression<Func<Bank, bool>> where)
        {            
            // Объединить выражение where с выражением отбора по имени.
            var nameExpession = where.CombineWith(i => i.Name == bank.Name);
            if (await bankRepository.AnyAsync(nameExpession))
                ModelState.AddError("Name", $"Банк с именем '{bank.Name}' уже существует.");

            var swiftExpression = where.CombineWith(i => i.SwiftCode == bank.SwiftCode);
            if (await bankRepository.AnyAsync(swiftExpression))
                ModelState.AddError("Name", $"Банк со SWIFT кодом '{bank.SwiftCode}' уже существует.");
        }
    }
}

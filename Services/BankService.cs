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
            // TODO: Выпихнуть результат проверки на верхний слой
            // для отображения на странице ошибок валидации и проверки на уникальность.
            ModelStateDictionary modelState = new ModelStateDictionary();
            if (!await IsUpdatable(updateDto, modelState))
                return false;

            Bank bankToUpdate = await bankRepository.GetSingleAsync(
                i => i.Id == updateDto.Id,
                i => i.ParsingSettings);

            if (bankToUpdate == null)
                throw new Exception($"Банк с Id '{updateDto.Id}' не найден.");

            updateDto.Adapt(bankToUpdate);
            bankRepository.SetStateModifed(bankToUpdate);
            await bankRepository.SaveChangesAsync();
            return true;
        }

        private async Task<bool> IsUpdatable(BankUpdateDto updateDto, ModelStateDictionary modelState) =>
            IsValid(updateDto, modelState) && (await IsUniqueForUpdate(updateDto, modelState));

        private async Task<bool> IsCreatable(BankCreateDto createDto, ModelStateDictionary modelState) =>
            IsValid(createDto, modelState) && (await IsUniqueForCreate(createDto, modelState));

        private bool IsValid(IUpdateCreateFields bank, ModelStateDictionary modelState)
        {
            // Проверить SwiftCode.
            if (string.IsNullOrEmpty(bank.SwiftCode))
                modelState.AddModelError("SwiftRequired", "SWIFT код обязателен.");
            if (string.IsNullOrEmpty(bank.SwiftCode) && bank.SwiftCode.Length != 11)
                modelState.AddModelError("SwiftLength", "Длина SWIFT кода составляет 11 символов.");

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

        private async Task<bool> IsUniqueForUpdate(BankUpdateDto updateDto, ModelStateDictionary modelState)
        {
            await UniqueCheck(updateDto, i => i.Id != updateDto.Id, modelState);
            return modelState.IsValid;
        }

        private async Task<bool> IsUniqueForCreate(BankCreateDto createDto, ModelStateDictionary modelState)
        {
            await UniqueCheck(createDto, null, modelState);
            return modelState.IsValid;
        }

        private async Task UniqueCheck(IUpdateCreateFields fields, Expression<Func<Bank, bool>> where, ModelStateDictionary modelState)
        {
            // Объединить два выражения Func: 
            Expression<Func<Bank, bool>> exprName = i => i.Name == fields.Name;
            var nameBody = Expression.AndAlso(where.Body, exprName.Body);
            var nameLambda = Expression.Lambda<Func<Bank, bool>>(nameBody, where.Parameters[0]);
            if (await bankRepository.AnyAsync(nameLambda))
                modelState.AddModelError("Name", $"Банк с именем '{fields.Name}' уже существует.");

            Expression<Func<Bank, bool>> exprSwift = i => i.SwiftCode == fields.SwiftCode;
            var swiftBody = Expression.AndAlso(where.Body, exprSwift.Body);
            var swiftLambda = Expression.Lambda<Func<Bank, bool>>(swiftBody, where.Parameters[0]);
            if (await bankRepository.AnyAsync(swiftLambda))
                modelState.AddModelError("Name", $"Банк со SWIFT кодом '{fields.SwiftCode}' уже существует.");
        }
    }
}

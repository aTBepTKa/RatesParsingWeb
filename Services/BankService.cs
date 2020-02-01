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

            // Присвоить целочисленной переменной i единицу.
            int i = 1;

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
        
        private async Task<bool> IsUpdatable(BankUpdateDto bankDto, ModelStateDictionary modelState) =>
            IsValid(bankDto, modelState) && (await IsUniqueForUpdate(bankDto, modelState));

        private async Task<bool> IsCreatable(BankUpdateDto bankDto, ModelStateDictionary modelState) =>
            IsValid(bankDto, modelState) && (await IsUniqueForCreate(bankDto, modelState));

        private bool IsValid(BankUpdateDto bankDto, ModelStateDictionary modelState)
        {
            // Проверить SwiftCode.
            if (string.IsNullOrEmpty(bankDto.SwiftCode))
                modelState.AddModelError("SwiftRequired", "SWIFT код обязателен.");
            if (bankDto.SwiftCode.Length != 11)
                modelState.AddModelError("SwiftLength", "Длина SWIFT кода составляет 11 символов.");

            // Проверить Name.
            if (string.IsNullOrEmpty(bankDto.Name))
                modelState.AddModelError("NameRequired", "Название банка обязательно.");
            if (bankDto.Name.Length > 50)
                modelState.AddModelError("NameLength", "Максимальная длина названия банка не более 50 символов.");

            // Проверить BankUrl.
            if (!Uri.TryCreate(bankDto.BankUrl, UriKind.Absolute, out _))
                modelState.AddModelError("BankUrl", "Ссылка страницы банка некорректна.");

            // Проверить RatesUrl.
            if (string.IsNullOrEmpty(bankDto.Name))
                modelState.AddModelError("NameRequired", "Страница курсов обязательна.");
            if (!Uri.TryCreate(bankDto.RatesUrl, UriKind.Absolute, out _))
                modelState.AddModelError("RatesUrl", "Ссылка страницы курсов банка некорректна.");
            return modelState.IsValid;
        }
        
        private async Task<bool> IsUniqueForUpdate(BankUpdateDto bankDto, ModelStateDictionary modelState)
        {
            var banksWithoutCurrent = await bankRepository.GetManyAsync(i => i.Id != bankDto.Id);
            UniqueCheck(bankDto, modelState, banksWithoutCurrent);
            return modelState.IsValid;
        }

        private async Task<bool> IsUniqueForCreate(BankUpdateDto bankDto, ModelStateDictionary modelState)
        {
            var banks = await bankRepository.GetAllAsync();
            UniqueCheck(bankDto, modelState, banks);
            return modelState.IsValid;
        }

        private void UniqueCheck(BankUpdateDto bankDto, ModelStateDictionary modelState, IEnumerable<Bank> banks)
        {
            if (banks.Any(i => i.Name == bankDto.Name))
                modelState.AddModelError("Name", $"Банк с именем '{bankDto.Name}' уже существует.");
            if (banks.Any(i => i.SwiftCode == bankDto.SwiftCode))
                modelState.AddModelError("Name", $"Банк со SWIFT кодом '{bankDto.SwiftCode}' уже существует.");
        }
    }
}

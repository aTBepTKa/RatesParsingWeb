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

        public async Task<IEnumerable<BankDto>> GetAll()
        {
            var banks = await BankRepository.GetAll();
            var bankDtoList = new List<BankDto>(banks.Count());
            foreach(var bank in banks)
            {                
                var newBankDto = bank.Adapt<BankDto>();
                newBankDto.CurrencyDto = bank.Currency.Adapt<CurrencyDto>();
                bankDtoList.Add(newBankDto);
            }
            return bankDtoList;
        }

        public async Task<bool> UpdateBankAsync(BankDto bankDto)
        {
            if (!IsValid(bankDto))
                return false;
            var bank = await BankRepository.GetByIdAsync(bankDto.Id);
            if (bank == null)
                return false;

            bank.Adapt(bankDto);
            BankRepository.SetStateModifed(bank);
            await BankRepository.SaveChangesAsync();
            return true;
        }

        public async Task<BankDto> GetByIdAsync(int id)
        {
            var bankDomain = await BankRepository.GetByIdAsync(id);
            if (bankDomain == null)
                throw new Exception($"Банк с ID = {id} не найден.");
            var bankDto = new BankDto();
            bankDto.Adapt(bankDomain);
            bankDto.CurrencyDto.Adapt(bankDomain.Currency);
            return bankDto;            
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using RatesParsingWeb.Domain;
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
    public class BankService : ServiceBase<Bank>, IBankService
    {
        private BankRepository BankRepository { get; set; }
        public BankService(BankRatesContext context)
        {
            BankRepository = new BankRepository(context);
            RepositoryBase = BankRepository;                 
        }

        public Task<Bank> GetByIdWithSettings(int id) =>
            BankRepository.GetByIdWithSettings(id);

        public override bool IsValid(Bank bank, ModelStateDictionary modelState)
        {
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
            if (!Uri.TryCreate(bank.BankUrl,UriKind.Absolute, out _))
                modelState.AddModelError("BankUrl", "Ссылка страницы банка некорректна.");

            // Проверить RatesUrl.
            if (string.IsNullOrEmpty(bank.Name))
                modelState.AddModelError("NameRequired", "Страница курсов обязательна.");
            if (!Uri.TryCreate(bank.RatesUrl, UriKind.Absolute, out _))
                modelState.AddModelError("RatesUrl", "Ссылка страницы курсов банка некорректна.");

            // Проверить CurrencyId.
            if (bank.CurrencyId == 0)
                modelState.AddModelError("CurrencyTextCode", "Основная валюта банка отсутствует в базе.");
            return modelState.IsValid;            
        }
    }
}

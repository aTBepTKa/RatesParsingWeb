using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Models;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RatesParsingWeb.Pages.Banks
{
    /// <summary>
    /// Средства для обработки данных банков.
    /// </summary>
    public class BaseBankPageModel : PageModel
    {
        /// <summary>
        /// Преобразовать репозиторий в модель представления.
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        protected BankModel GetBankModel(Bank bank) =>
            new BankModel
            {
                Id = bank.Id,
                Name = bank.Name,
                BankUrl = bank.BankUrl,
                RatesUrl = bank.RatesUrl,
                CurrencyId = bank.CurrencyId,
                CurrencyName = bank.Currency.Name,
                CurrencyTextCode = bank.Currency.TextCode
                // Прикольная лесенка получилась.
            };
        protected Bank GetBankDomain(BankModel bank) =>
            new Bank
            {
                Id = bank.Id,
                Name = bank.Name,
                BankUrl = bank.BankUrl,
                RatesUrl = bank.RatesUrl,
                CurrencyId = bank.CurrencyId
            };
    }
}

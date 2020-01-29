using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RatesParsingWeb.Models;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Dto;
using Mapster;

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
        protected BankModel GetBankModel(BankDto bank) =>
             bank.Adapt<BankModel>();


    }
}

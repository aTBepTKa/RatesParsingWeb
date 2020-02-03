using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.UpdateAndCreate
{
    interface IUpdateCreateFields
    {
        string SwiftCode { get; set; }

        /// <summary>
        /// Название банка.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Ссылка на главную страницу банка.
        /// </summary>
        string BankUrl { get; set; }

        /// <summary>
        /// Ссылка на страницу, содержащую обменные курсы валют.
        /// </summary>
        string RatesUrl { get; set; }

        /// <summary>
        /// Основная валюта банка.
        /// </summary>
        int CurrencyId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.UpdateAndCreate
{
    public class BankUpdateDto : IBankRequisites
    {
        public int Id { get; set; }
        /// <summary>
        /// SWIFT код банка.
        /// </summary>
        public string SwiftCode { get; set; }

        /// <summary>
        /// Название банка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на главную страницу банка.
        /// </summary>
        public string BankUrl { get; set; }

        /// <summary>
        /// Ссылка на страницу, содержащую обменные курсы валют.
        /// </summary>
        public string RatesUrl { get; set; }

        /// <summary>
        /// Основная валюта банка.
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Настройки парсинга банка.
        /// </summary>
        public ParsingSettingsDto ParsingSettings { get; set; }
    }
}

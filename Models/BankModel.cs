using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class BankModel
    {
        public int Id { get; set; }

        /// <summary>
        /// SWIFT код банка.
        /// </summary>
        [DisplayName("SWIFT код банка")]
        public string SwiftCode { get; set; }

        /// <summary>
        /// Название банка.
        /// </summary>
        [DisplayName("Название банка")]
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на главную страницу банка.
        /// </summary>
        [DisplayName("Страница банка")]
        public string BankUrl { get; set; }

        /// <summary>
        /// Ссылка на страницу, содержащую обменные курсы валют.
        /// </summary>
        [DisplayName("Страница курсов")]
        public string RatesUrl { get; set; }

        /// <summary>
        /// Ссылка на основную валюту банка.
        /// </summary>
        [DisplayName("Цифровой код валюты")]
        public int CurrencyId { get; set; }

        /// <summary>
        /// Наименование валюты.
        /// </summary>
        [DisplayName("Наименование валюты")]
        public string CurrencyName { get; set; }

        /// <summary>
        /// Текстовый код валюты.
        /// </summary>
        [DisplayName("Код валюты")]
        public string CurrencyTextCode { get; set; }
    }
}

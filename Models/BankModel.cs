using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class BankModel
    {
        public int Id { get; set; }

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
        /// Ссылка на основную валюту банка.
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Наименование валюты.
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// Текстовый код валюты.
        /// </summary>
        public string CurrencyTextCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Представляет данные банка с данными для выполнения парсинга страниц и результатами парсинга.
    /// </summary>
    public class Bank
    {
        public int Id { get; set; }

        /// <summary>
        /// Название банка.
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Ссылка на главную страницу банка.
        /// </summary>
        public string BankUrl { get; set; }

        /// <summary>
        /// Ссылка на страницу, содержащую обменные курсы валют.
        /// </summary>
        public string RatesUrl { get; set; }
        

        public virtual int CurrencyID { get; set; }

        /// <summary>
        /// Обменные курсы банка (результаты парсинга).
        /// </summary>
        public virtual ICollection<ExchangeRateList> ExchangeRateLists { get; set; }

        /// <summary>
        /// Настройки парсинга страницы.
        /// </summary>
        public virtual ParsingSettings ParsingSettings { get; set; }

        /// <summary>
        /// Основная валюта банка.
        /// </summary>
        public virtual Currency Currency { get; set; }
    }
}

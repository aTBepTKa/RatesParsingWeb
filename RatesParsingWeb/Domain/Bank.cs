﻿using System;
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


        

        public int CurrencyId { get; set; }

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

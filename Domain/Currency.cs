﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    // ISO 4217.
    /// <summary>
    /// Данные валюты.
    /// </summary>
    public class Currency
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование валюты.
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// Текстовый код валюты.
        /// </summary>
        public string TextCode { get; set; }

        /// <summary>
        /// Цифровой код валюты.
        /// </summary>
        public int NumCode { get; set; }
    }
}

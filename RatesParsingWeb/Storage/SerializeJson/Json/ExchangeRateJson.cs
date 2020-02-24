using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.SerializeJson.Json
{
    /// <summary>
    /// Обменный курс валюты.
    /// </summary>
    public class ExchangeRateJson
    {
        /// <summary>
        /// Единица измерения валюты.
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// Значение обменного курса валюты.
        /// </summary>
        public decimal ExchangeRateValue { get; set; }

        /// <summary>
        /// Код валюты.
        /// </summary>
        public string TextCode { get; set; }
    }
}

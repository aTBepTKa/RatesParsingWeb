using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages
{
    /// <summary>
    /// Данные валюты.
    /// </summary>
    public interface IExchangeRate
    {
        /// <summary>
        /// Сокращенное название валюты.
        /// </summary>
        string TextCode { get; set; }

        /// <summary>
        /// Единица измерения валюты.
        /// </summary>
        int Unit { get; set; }

        /// <summary>
        /// Обменный курс валюты.
        /// </summary>
        decimal ExchangeRateValue { get; set; }
    }
}

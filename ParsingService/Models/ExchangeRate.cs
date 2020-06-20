using ParsingMessages.Parsing;

namespace ParsingService.Models
{
    /// <summary>
    /// Содержит данные о валюте.
    /// </summary>
    public class ExchangeRate : IExchangeRate
    {
        /// <summary>
        /// Сокращенное название валюты.
        /// </summary>
        public string TextCode { get; set; }

        /// <summary>
        /// Единица измерения валюты.
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// Обменный курс валюты.
        /// </summary>
        public decimal ExchangeRateValue { get; set; }
    }
}

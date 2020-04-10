using ParsingMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    /// <summary>
    /// Результат выполнения парсинга.
    /// </summary>
    class ParsingResult : ResponseBase
    {
        /// <summary>
        /// Список обменных курсов.
        /// </summary>
        public IEnumerable<ExchangeRate> ExchangeRates { get; set; }
    }
}

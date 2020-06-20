using ParsingMessages;
using ParsingMessages.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    /// <summary>
    /// Результат выполнения парсинга.
    /// </summary>
    class ParsingResult : ResponseBase, IResponsable<IExchangeRate>
    {
        /// <summary>
        /// Список обменных курсов.
        /// </summary>
        public IEnumerable<IExchangeRate> Message { get; set; }
    }
}

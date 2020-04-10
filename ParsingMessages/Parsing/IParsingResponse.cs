using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages.Parsing
{
    /// <summary>
    /// Ответ на запрос. Возвращает коллекцию обменных курсов по банку.
    /// </summary>
    public interface IParsingResponse : IResponsable
    {
        /// <summary>
        /// Список валют банка с обменными курсами.
        /// </summary>
        IEnumerable<IExchangeRate> ExchangeRates { get; set; }
    }
}

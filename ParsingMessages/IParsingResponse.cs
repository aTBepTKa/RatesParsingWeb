using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages
{
    /// <summary>
    /// Ответ на запрос. Возвращает коллекцию обменных курсов по банку.
    /// </summary>
    public interface IParsingResponse
    {
        /// <summary>
        /// Список валют банка с обменными курсами.
        /// </summary>
        IEnumerable<IExchangeRate> ExchangeRates { get; set; }

        /// <summary>
        /// Успешность выполнения парсинга.
        /// </summary>
        bool IsSuccefullParsed { get; set; }

        /// <summary>
        /// Список ошибок при выполнении парсинга.
        /// </summary>
        Dictionary<string, IEnumerable<string>> ErrorDictionary { get; set; }
    }
}

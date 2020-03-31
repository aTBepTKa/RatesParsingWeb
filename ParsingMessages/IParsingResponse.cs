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
        bool IsSuccesfullParsed { get; set; }

        /// <summary>
        /// Текст ошибки при выполнении парсинга.
        /// </summary>
        string ErrorDescription { get; set; }
    }
}

using ParsingMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    /// <summary>
    /// Результат выполнения парсинга.
    /// </summary>
    class ParsingResult
    {
        /// <summary>
        /// Список обменных курсов.
        /// </summary>
        public IEnumerable<ExchangeRate> ExchangeRates { get; set; }

        /// <summary>
        /// Парсинг выполнен успешно.
        /// </summary>
        public bool IsSuccesfullParsed { get; private set; } = true;

        /// <summary>
        /// Описание ошибки.
        /// </summary>
        public string ErrorDescription { get; private set; }

        /// <summary>
        /// Задать ошибку.
        /// </summary>
        /// <param name="errorMessage">Текст ошибки.</param>
        public void SetError(string errorMessage)
        {
            ErrorDescription = errorMessage;
            IsSuccesfullParsed = false;
        }
    }
}

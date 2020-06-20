using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    /// <summary>
    /// Базовые команды для реализации класса ответа на запрос.
    /// </summary>
    public abstract class ResponseBase
    {
        /// <summary>
        /// Парсинг выполнен успешно.
        /// </summary>
        public bool IsSuccesfullParsed { get; set; } = true;

        /// <summary>
        /// Описание ошибки.
        /// </summary>
        public string ErrorDescription { get; set; }

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

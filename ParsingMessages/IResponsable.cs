using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages
{
    /// <summary>
    /// Представляет ответ на запрос.
    /// </summary>
    public interface IResponsable<T>
    {
        /// <summary>
        /// Успешность обработки запроса.
        /// </summary>
        bool IsSuccesfullParsed { get; set; }

        /// <summary>
        /// Текст ошибки при выполнении обработки запроса.
        /// </summary>
        string ErrorDescription { get; set; }

        /// <summary>
        /// Содержание ответа на запрос.
        /// </summary>
        IEnumerable<T> Message { get; set; }
    }
}

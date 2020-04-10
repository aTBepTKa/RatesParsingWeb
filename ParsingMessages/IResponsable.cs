using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages
{
    /// <summary>
    /// Объект предназначен для направления ответа на запрос.
    /// </summary>
    public interface IResponsable
    {
        /// <summary>
        /// Успешность обработки запроса.
        /// </summary>
        bool IsSuccesfullParsed { get; set; }

        /// <summary>
        /// Текст ошибки при выполнении обработки запроса.
        /// </summary>
        string ErrorDescription { get; set; }
    }
}

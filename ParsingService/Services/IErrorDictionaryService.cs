using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsingService.Services
{
    /// <summary>
    /// Представляет словарь полей с соответствующим списком ошибок для каждого поля.
    /// </summary>
    public interface IErrorDictionaryService
    {
        /// <summary>
        /// Добавить ошибку.
        /// </summary>
        /// <param name="key">Наименование свойства с ошибкой.</param>
        /// <param name="value">Текст ошибки.</param>
        void AddError(string key, string value);

        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        Dictionary<string,IEnumerable<string>> ErrorDictionary { get; }

        /// <summary>
        /// Получить список ошибок с ключами (именами свойств).
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> ErrorListWithKeys { get; }

        /// <summary>
        /// Получить список ошибок без полей (имен свойств).
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> ErrorListWithoutKeys { get; }

        /// <summary>
        /// Валидация прошла успешно.
        /// </summary>
        bool IsValid { get; }
    }
}

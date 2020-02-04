using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    /// <summary>
    /// Представляет словарь полей с соответствующим списком ошибок для каждого поля.
    /// </summary>
    public interface IValidationDictionary
    {
        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        IDictionary<string, List<string>> ErrorDictioanry { get; }

        /// <summary>
        /// Добавить ошибку.
        /// </summary>
        /// <param name="key">Наименование свойства с ошибкой.</param>
        /// <param name="value">Текст ошибки.</param>
        void AddError(string key, string value);

        /// <summary>
        /// Получить список ошибок с ключами (именами свойств).
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetErrorListWithKeys();

        /// <summary>
        /// Получить список ошибок без полей (имен свойств).
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetErrorListWithoutKeys();

        /// <summary>
        /// Валидация прошла успешно.
        /// </summary>
        bool IsValid { get; }
    }
}

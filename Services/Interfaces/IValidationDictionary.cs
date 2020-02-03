using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IValidationDictionary
    {
        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        IDictionary<string, string> ErrorDictioanry { get; }

        /// <summary>
        /// Добавить ошибку.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddError(string key, string value);

        /// <summary>
        /// Валидация прошла успешно.
        /// </summary>
        bool IsValid { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ParsingService.Models;

namespace ParsingService
{
    /// <summary>
    /// Команды для выполнения обработки строки.
    /// </summary>
    class Commands
    {
        /// <summary>
        /// Получить число из текста.
        /// </summary>
        /// <param name="text">Исходный текст.</param>
        /// <returns></returns>
        public WordProcessingHandler GetNumberFromText()
        {
            string handler(string text)
            {
                string digitText = "";

                foreach (char ch in text)
                {
                    if (char.IsDigit(ch))
                        digitText += ch;
                }
                return digitText;
            }
            return handler;
        }

        /// <summary>
        /// Получить строку заданной длины начиная с конца исходной строки.
        /// </summary>
        /// <param name="length">Длина строки в формате string.</param>
        /// <returns></returns>
        public WordProcessingHandler GetTextFromEnd(string length)
        {
            if (!int.TryParse(length, out int newLength))
                newLength = 0;

            string handler(string text)
            {
                return text.Substring(text.Length - newLength);
            }
            return handler;
        }

        /// <summary>
        /// Найти и заменить строку в тексте. 
        /// </summary>
        /// <param name="oldText">Заменяемая строка.</param>
        /// <param name="newText">Новая строка.</param>
        /// <returns></returns>
        public WordProcessingHandler ReplaceSubstring(string oldText, string newText)
        {
            string handler(string text)
            {
                return text.Replace(oldText, newText);
            }
            return handler;
        }

        /// <summary>
        /// Получить фиксированное значение независимо от получаемой строки.
        /// </summary>
        /// <param name="value">Значение фиксированного значения.</param>
        /// <returns></returns>
        public WordProcessingHandler GetFixedValue(string value)
        {
            string handler(string text)
            {
                return value;
            }
            return handler;
        }
    }
}

namespace ParsingService.Services
{
    /// <summary>
    /// Команды для выполнения обработки строки.
    /// </summary>
    class Commands
    {
        /// <summary>
        /// !!! Получить число из текста.
        /// </summary>
        [Command("GetNumberFromText", "Получить число из текста.")]
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
        [Command("GetTextFromEnd", "Получить строку заданной длины начиная с конца исходной строки.")]
        [Parameter("length", "Длина строки в формате string.")]
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
        [Command("ReplaceSubstring", "Найти и заменить строку в тексте.")]
        [Parameter("oldText", "Заменяемая строка.")]
        [Parameter("newText", "Новая строка.")]
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
        [Command("GetFixedValue", "Получить фиксированное значение независимо от получаемой строки.")]
        [Parameter("value", "Значение фиксированного значения.")]
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

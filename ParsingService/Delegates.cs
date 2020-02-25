using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService
{
    /// <summary>
    /// Обработчик строки. Применяется для обертывания команды, которая выполняет обработку строки.
    /// </summary>
    /// <param name="text">Исходный строка для обработки.</param>
    /// <returns></returns>
     delegate string WordProcessingHandler(string text);
}

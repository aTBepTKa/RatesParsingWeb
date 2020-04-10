using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService
{
    /// <summary>
    /// Средства для отображения лога в консоли.
    /// </summary>
    public static class ConsoleLog
    {
        /// <summary>
        /// Вывести сообщение в консоль.
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMessage(string message)
        {
            var dateTimeNow = DateTime.Now;
            var showString = string.Concat(dateTimeNow, ". ", message);
            Console.WriteLine(showString);
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.CommandService
{
    /// <summary>
    /// Результат запроса получения команд обработки текста.
    /// </summary>
    public class CommandResultDto
    {
        /// <summary>
        /// Успешность обработки запроса.
        /// </summary>
        public bool IsSuccesfullParsed { get; set; }

        /// <summary>
        /// Текст ошибки при выполнении обработки запроса.
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Список команд обработки текста.
        /// </summary>
        public IEnumerable<CommandDto> Commands { get; set; }
    }
}

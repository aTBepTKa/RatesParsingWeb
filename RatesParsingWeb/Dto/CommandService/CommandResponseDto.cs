using System.Collections.Generic;

namespace RatesParsingWeb.Dto.CommandService
{
    /// <summary>
    /// Результат запроса получения команд обработки текста.
    /// </summary>
    public class CommandResponseDto
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
        public IEnumerable<ExternalCommandDto> Message { get; set; }

    }
}

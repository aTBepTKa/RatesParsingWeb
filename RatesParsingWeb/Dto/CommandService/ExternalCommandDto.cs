using System.Collections.Generic;

namespace RatesParsingWeb.Dto.CommandService
{
    /// <summary>
    /// Команда, полученная из внешнего сервиса.
    /// </summary>
    public class ExternalCommandDto
    {
        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание команды.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Параметры команды.
        /// </summary>
        public IEnumerable<ExternalParameterDto> CommandParameters { get; set; }
    }
}

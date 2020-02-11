using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.ExternalCommand
{
    /// <summary>
    /// Команда полученная из внешнего источника.
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
        public ICollection<ExternalCommandParameterDto> CommandParameters { get; set; }
    }
}

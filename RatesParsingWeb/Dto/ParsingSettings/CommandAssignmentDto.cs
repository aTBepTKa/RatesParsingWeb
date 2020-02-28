using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.ParsingSettings
{
    /// <summary>
    /// Содержит команду с параметрами для обработки строки.
    /// </summary>
    public class CommandAssignmentDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя поля для которого назначается команда обработки текста.
        /// </summary>
        public virtual AssignmentFieldNameDto AssignmentFieldName { get; set; }

        /// <summary>
        /// Наименование команды.
        /// </summary>
        public virtual CommandDto Command { get; set; }

        /// <summary>
        /// Значения параметров команды.
        /// </summary>
        public virtual ICollection<CommandParameterValueDto> CommandParameterValues { get; set; }
    }
}

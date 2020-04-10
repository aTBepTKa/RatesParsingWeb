using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models.ParsingSettings
{
    /// <summary>
    /// Содержит команду с параметрами для обработки строки.
    /// </summary>
    public class CommandAssignmentModel
    {
        /// <summary>
        /// Имя поля для которого назначается команда обработки текста.
        /// </summary>
        public virtual AssignmentFieldNameModel AssignmentFieldName { get; set; }

        /// <summary>
        /// Наименование команды.
        /// </summary>
        public virtual CommandModel Command { get; set; }

        /// <summary>
        /// Значения параметров команды.
        /// </summary>
        public virtual ICollection<CommandParameterValueModel> CommandParameterValues { get; set; }
    }
}

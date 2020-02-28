using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Содержит команду с параметрами для обработки строки.
    /// </summary>
    public class CommandAssignment
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя поля для которого назначается команда обработки текста.
        /// </summary>
        public virtual AssignmentFieldName AssignmentFieldName { get; set; }

        /// <summary>
        /// Наименование команды.
        /// </summary>
        public virtual Command Command { get; set; }

        /// <summary>
        /// Настройки парсинга.
        /// </summary>
        public virtual ParsingSettings ParsingSettings { get; set; }

        /// <summary>
        /// Значения параметров команды.
        /// </summary>
        public virtual ICollection<CommandParameterValue> CommandParameterValues { get; set; }

        public virtual int AssignmentFieldNameId { get; set; }
        public virtual int CommandId { get; set; }
        public virtual int ParsingSettingsId { get; set; }
    }
}

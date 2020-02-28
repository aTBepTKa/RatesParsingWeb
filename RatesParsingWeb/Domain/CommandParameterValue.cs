using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Параметр команды для выполнения парсинга.
    /// </summary>
    public class CommandParameterValue
    {
        public int Id { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Назначение команды.
        /// </summary>
        public virtual CommandAssignment CommandAssignment { get; set; }

        /// <summary>
        /// Параметр команды.
        /// </summary>
        public virtual CommandParameter CommandParameter { get; set; }

        public virtual int CommandParameterId { get; set; }
        public virtual int CommandAssignmentId { get; set; }
    }
}

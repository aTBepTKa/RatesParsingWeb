using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Имя поля для которого назначается команда обработки текста.
    /// </summary>
    public class AssignmentFieldName
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя поля.
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<CommandAssignment> CommandAssignments { get; set; }
    }
}

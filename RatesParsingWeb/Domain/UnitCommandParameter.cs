using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Параметр команды для выполнения парсинга.
    /// </summary>
    public class UnitCommandParameter
    {
        public int Id { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }

        public int UnitCommandAssignmentId { get; set; }

        /// <summary>
        /// Ссылка на команду.
        /// </summary>
        public virtual UnitCommandAssignment UnitCommandAssignment { get; set; }
    }
}

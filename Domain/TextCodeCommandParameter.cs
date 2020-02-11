using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Параметр команды для выполнения парсинга.
    /// </summary>
    public class TextCodeCommandParameter
    {
        public int Id { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }

        public int TextCodeCommandAssignmentId { get; set; }

        /// <summary>
        /// Ссылка на команду.
        /// </summary>
        public virtual TextCodeCommandAssignment TextCodeCommandAssignment { get; set; }
    }
}

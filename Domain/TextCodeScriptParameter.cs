using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Параметр скрипта для выполнения парсинга.
    /// </summary>
    public class TextCodeScriptParameter
    {
        public int Id { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }

        public int TextCodeScriptAssignmentId { get; set; }

        /// <summary>
        /// Ссылка на скрипт.
        /// </summary>
        public virtual TextCodeScriptAssignment TextCodeScriptAssignment { get; set; }
    }
}

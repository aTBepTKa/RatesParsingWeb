using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models.ParsingSettings
{
    /// <summary>
    /// Параметр команды для выполнения парсинга.
    /// </summary>
    public class CommandParameterValueModel
    {
        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Параметр команды.
        /// </summary>
        public virtual CommandParameterModel CommandParameter { get; set; }
    }
}

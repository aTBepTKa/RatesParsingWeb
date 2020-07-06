using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models.ParsingSettings
{
    /// <summary>
    /// Значение параметра команды. Используется для изменения значения параметра.
    /// </summary>
    public class CommandParameterValueModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }
    }
}

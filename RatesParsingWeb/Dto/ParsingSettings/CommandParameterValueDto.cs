using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.ParsingSettings
{
    /// <summary>
    /// Параметр команды для выполнения парсинга.
    /// </summary>
    public class CommandParameterValueDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Параметр команды.
        /// </summary>
        public virtual CommandParameterDto CommandParameter { get; set; }
    }
}

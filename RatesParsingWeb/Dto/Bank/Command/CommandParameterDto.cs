using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.Bank.Command
{
    /// <summary>
    /// Параметр команды.
    /// </summary>
    public class CommandParameterDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование параметра.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание параметра.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }
    }
}

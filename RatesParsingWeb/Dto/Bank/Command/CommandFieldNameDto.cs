using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.Bank.Command
{
    public class CommandFieldNameDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя поля.
        /// </summary>
        public string Name { get; set; }
    }
}

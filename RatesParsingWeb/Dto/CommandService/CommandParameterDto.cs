using ParsingMessages.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.CommandService
{
    public class CommandParameterDto : ICommandParameter
    {
        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полное наименование команды для пользователя.
        /// </summary>
        public string Description { get; set; }
    }
}

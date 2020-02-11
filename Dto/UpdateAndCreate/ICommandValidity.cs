using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto
{
    public interface ICommandValidity
    {
        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Наименование команды для пользователя.
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// Описание команды.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Параметры команды.
        /// </summary>
        ICollection<CommandParameterDto> CommandParameters { get; set; }
    }
}

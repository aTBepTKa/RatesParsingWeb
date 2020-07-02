using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models.ParsingSettings
{
    public class CommandParameterModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полное наименование команды для пользователя.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public string Value { get; set; }
    }
}

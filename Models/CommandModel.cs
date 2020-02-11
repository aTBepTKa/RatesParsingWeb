using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class CommandModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        [DisplayName("Наименование команды")]
        public string Name { get; set; }

        /// <summary>
        /// Описание команды.
        /// </summary>
        [DisplayName("Описание команды")]
        public string Description { get; set; }

        /// <summary>
        /// Параметры команды.
        /// </summary>
        [DisplayName("Параметры команды")]
        public ICollection<CommandParameterModel> CommandParameters { get; set; }
    }
}

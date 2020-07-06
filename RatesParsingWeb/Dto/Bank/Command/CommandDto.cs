using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.Bank.Command
{
    /// <summary>
    /// Команда для обработки текста.
    /// </summary>
    public class CommandDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание команды.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Параметры команды.
        /// </summary>
        public ICollection<CommandParameterDto> CommandParameters { get; set; }
        /// <summary>
        /// Имя поля для которого назначается команда обработки текста.
        /// </summary>

        public virtual CommandFieldNameDto CommandFieldName { get; set; }
    }
}

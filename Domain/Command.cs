using System.Collections.Generic;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Команда для обработки текста.
    /// </summary>
    public class Command
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
        public ICollection<CommandParameter> CommandParameters { get; set; }

        /// <summary>
        /// Назначение команды для единицы измерения.
        /// </summary>
        public virtual ICollection<UnitCommandAssignment> UnitCommandAssignments { get; set; }

        /// <summary>
        /// Назначение команды для текстового кода.
        /// </summary>
        public virtual ICollection<TextCodeCommandAssignment> TextCodeCommandAssignments { get; set; }
    }
}

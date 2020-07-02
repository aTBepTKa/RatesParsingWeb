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
        /// Имя поля для которого назначается команда обработки текста.
        /// </summary>
        public virtual CommandFieldName CommandFieldName { get; set; }

        /// <summary>
        /// Настройки парсинга.
        /// </summary>
        public virtual ParsingSettings ParsingSettings { get; set; }


        public virtual int CommandFieldNameId { get; set; }
        public virtual int ParsingSettingsId { get; set; }
    }
}

using System.Collections.Generic;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Имя поля для которого назначается команда обработки текста.
    /// </summary>
    public class CommandFieldName
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя поля.
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<Command> Commands { get; set; }
    }
}

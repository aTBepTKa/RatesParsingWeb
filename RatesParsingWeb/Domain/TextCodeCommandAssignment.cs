using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Содержит команду с параметрами для обработки строки текстового кода валюты (TextCode), полученной непосредственно после парсинга.
    /// </summary>
    public class TextCodeCommandAssignment
    {
        public int Id { get; set; }
        public virtual int CommandId { get; set; }
        public virtual int ParsingSettingsId { get; set; }

        /// <summary>
        /// Наименование команды.
        /// </summary>
        public virtual Command Command { get; set; }

        /// <summary>
        /// Банк, для которого выполняется команда.
        /// </summary>
        public virtual ParsingSettings ParsingSettings { get; set; }

        /// <summary>
        /// Параметры команды.
        /// </summary>
        public virtual ICollection<TextCodeCommandParameter> CommandParameters { get; set; }
    }
}

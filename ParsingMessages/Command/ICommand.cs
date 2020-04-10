using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages.Command
{
    /// <summary>
    /// Содержит команду обработки текста.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Наименование команды.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Описание команды.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Параметры команды.
        /// </summary>
        IEnumerable<ICommandParameter> CommandParameters { get; set; }
    }
}

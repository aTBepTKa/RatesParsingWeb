using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages.Command
{
    public interface ICommandResponse : IResponsable
    {
        /// <summary>
        /// Список команд обработки текста.
        /// </summary>
        IEnumerable<ICommand> Commands { get; set; }
    }
}

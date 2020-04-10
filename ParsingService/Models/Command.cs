using ParsingMessages.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    /// <summary>
    /// Команда обработки текста.
    /// </summary>
    class Command
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<CommandParameter> CommandParameters { get; set; }
    }
}

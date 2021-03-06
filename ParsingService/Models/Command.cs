﻿using ParsingMessages.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    /// <summary>
    /// Команда обработки текста.
    /// </summary>
    class Command : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ICommandParameter> CommandParameters { get; set; }
    }
}

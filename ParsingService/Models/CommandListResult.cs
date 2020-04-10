using ParsingMessages.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    class CommandListResult : ResponseBase
    {
        public IEnumerable<Command> Commands { get; set; }
    }
}

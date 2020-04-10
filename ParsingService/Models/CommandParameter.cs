using ParsingMessages.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    public class CommandParameter : ICommandParameter
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

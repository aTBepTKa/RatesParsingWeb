using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages.Command
{
    public interface ICommandRequest
    {
        string TaskName { get; set; }
    }
}

using ParsingMessages.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.CommandService
{
    public class CommandRequest : ICommandRequest
    {
        public CommandRequest(string taskName)
        {
            TaskName = taskName;
        }
        public string TaskName { get; set; }
    }
}

﻿using ParsingMessages;
using ParsingMessages.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    class CommandListResult : ResponseBase, ICommandResponse
    {
        public IEnumerable<ICommand> Message { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Параметр команды.
    /// </summary>
    public class CommandParameter
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование параметра.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание параметра.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Команда обработки текста.
        /// </summary>
        public virtual Command Command { get; set; }

        /// <summary>
        /// Значения параметра.
        /// </summary>
        public virtual ICollection<CommandParameterValue> CommandParameterValues { get; set; }

        public virtual int CommandId { get; set; }
    }
}

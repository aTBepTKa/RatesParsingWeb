﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Параметр для команды.
    /// </summary>
    public class CommandParameter
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полное наименование команды для пользователя.
        /// </summary>
        public string Description { get; set; }


        public virtual int CommandId { get; set; }
        public virtual Command Command { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.CommandService
{
    public class CommandDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание команды.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Параметры команды.
        /// </summary>
        public IEnumerable<CommandParameterDto> CommandParameters { get; set; }
    }
}
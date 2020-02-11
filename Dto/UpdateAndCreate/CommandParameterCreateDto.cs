﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.UpdateAndCreate
{
    public class CommandParameterCreateDto
    {
        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полное наименование команды для пользователя.
        /// </summary>
        public string FullName { get; set; }
    }
}

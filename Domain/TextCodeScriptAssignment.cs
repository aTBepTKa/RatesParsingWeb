﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Содержит скрипт с параметрами для обработки строки текстового кода валюты (TextCode), полученной непосредственно после парсинга.
    /// </summary>
    public class TextCodeScriptAssignment
    {
        public int Id { get; set; }
        public virtual int ScriptId { get; set; }
        public virtual int BankId { get; set; }

        /// <summary>
        /// Наименование скрипта.
        /// </summary>
        public virtual Script Script { get; set; }

        /// <summary>
        /// Банк, для которого выполняется скрипт.
        /// </summary>
        public virtual Bank Bank { get; set; }

        /// <summary>
        /// Параметры скрипта.
        /// </summary>
        public virtual ICollection<TextCodeScriptParameter> ScriptParameters { get; set; }
    }
}

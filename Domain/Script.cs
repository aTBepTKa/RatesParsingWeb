using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Скрипт для обработки строки.
    /// </summary>
    public class Script
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование скрипта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество параметров скрипта.
        /// </summary>
        public int ParamsNum { get; set; }

        /// <summary>
        /// Ссылка на скрипт с параметрами.
        /// </summary>
        public virtual ICollection<UnitScriptAssignment> ScriptAssignments { get; set; }
    }
}

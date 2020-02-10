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
        /// Наименование скрипта для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Наименование скрипта для пользователя.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Описание скрипта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Параметры скрипта.
        /// </summary>
        public ICollection<ScriptParameter> ScriptParameter { get; set; }

        /// <summary>
        /// Назначение скрипта для единицы измерения.
        /// </summary>
        public virtual ICollection<UnitScriptAssignment> UnitScriptAssignments{ get; set; }

        /// <summary>
        /// Назначение скрипта для текстового кода.
        /// </summary>
        public virtual ICollection<TextCodeScriptAssignment> TextCodeScriptAssignments { get; set; }
    }
}

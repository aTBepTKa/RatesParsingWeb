using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Параметр для скрипта.
    /// </summary>
    public class ScriptParameter
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование скрипта для дальнейшей работы с рефлексией.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полное наименование скрипта для пользователя.
        /// </summary>
        public string FullName { get; set; }


        public virtual int ScriptId { get; set; }
        public virtual Script Script { get; set; }
    }
}

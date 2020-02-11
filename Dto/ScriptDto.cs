using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto
{
    public class ScriptDto
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
        public ICollection<ScriptParameterDto> ScriptParameters { get; set; }
    }
}

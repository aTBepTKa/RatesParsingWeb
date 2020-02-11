using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ScriptModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование скрипта для дальнейшей работы с рефлексией.
        /// </summary>
        [DisplayName("Наименование скрипта")]
        public string Name { get; set; }

        /// <summary>
        /// Наименование скрипта для пользователя.
        /// </summary>
        [DisplayName("Полное наименование скрипта")]
        public string FullName { get; set; }

        /// <summary>
        /// Описание скрипта.
        /// </summary>
        [DisplayName("Описание скрипта")]
        public string Description { get; set; }

        /// <summary>
        /// Параметры скрипта.
        /// </summary>
        [DisplayName("Параметры скрипта")]
        public ICollection<ScriptParameterModel> ScriptParameters { get; set; }
    }
}

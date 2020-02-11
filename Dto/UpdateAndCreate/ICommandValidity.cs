using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto
{
    /// <summary>
    /// Представляет свойства подлежащие валидации.
    /// </summary>
    public interface ICommandValidity
    {
        /// <summary>
        /// Наименование команды для дальнейшей работы с рефлексией.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Описание команды.
        /// </summary>
        string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class CurrencySelectListModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование валюты.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текстовый код валюты.
        /// </summary>
        public string TextCode { get; set; }

        /// <summary>
        /// Код валюты с ее полным наименованием.
        /// </summary>
        public string CodeWithName =>
            $"{TextCode} - {Name}";
    }
}

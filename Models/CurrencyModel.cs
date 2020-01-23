using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class CurrencyModel
    {
        [DisplayName("Цифровой код валюты")]
        public int Id { get; set; }

        /// <summary>
        /// Наименование валюты.
        /// </summary>
        [DisplayName("Наименование валюты")]
        public string Name { get; set; }

        /// <summary>
        /// Текстовый код валюты.
        /// </summary>
        [DisplayName("Код валюты")]
        public string TextCode { get; set; }

        /// <summary>
        /// Код валюты с ее полным наименованием.
        /// </summary>
        [DisplayName("Наименование валюты")]
        public string CodeWithName =>
            $"{TextCode} - {Name}";


    }
}

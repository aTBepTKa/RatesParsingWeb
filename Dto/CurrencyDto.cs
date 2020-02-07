using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto
{
    public class CurrencyDto
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

    }
}

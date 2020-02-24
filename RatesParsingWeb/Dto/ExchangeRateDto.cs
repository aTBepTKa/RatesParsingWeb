using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto
{
    public class ExchangeRateDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Единица измерения валюты.
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// Значение обменного курса валюты.
        /// </summary>
        public decimal ExchangeRateValue { get; set; }


        public int CurrencyId { get; set; }

        public int ExchangeRateListId { get; set; }

        /// <summary>
        /// Тип валюты.
        /// </summary>
        public virtual CurrencyDto Currency { get; set; }
    }
}

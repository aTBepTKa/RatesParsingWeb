using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ExchangeRateModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Единица измерения валюты.
        /// </summary>
        [DisplayName("Единица измерения")]
        public int Unit { get; set; }

        /// <summary>
        /// Значение обменного курса валюты.
        /// </summary>
        [DisplayName("Курс")]
        public decimal ExchangeRateValue { get; set; }

        /// <summary>
        /// Тип валюты.
        /// </summary>
        public virtual CurrencyModel Currency { get; set; }
    }
}

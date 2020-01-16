using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Domain
{
    /// <summary>
    /// Обменный курс валюты.
    /// </summary>
    public class ExchangeRate
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

        
        public int CurrencyID { get; set; }

        public int ExchangeRateListId { get; set; }

        /// <summary>
        /// Ссылка на список всех обменных курсов банка.
        /// </summary>
        public virtual ExchangeRateList ExchangeRateList { get; set; }
        /// <summary>
        /// Тип валюты.
        /// </summary>
        public virtual Currency Currency { get; set; }
    }
}

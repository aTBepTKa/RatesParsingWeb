using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ExchangeRate
    {
        public int ID { get; set; }
        public int ExchangeRateListID { get; set; }
        public int CurrencyID { get; set; }
        public int Unit { get; set; }
        public decimal ExchangeRateValue { get; set; }

        public Currency Currency { get; set; }
        public ExchangeRateList ExchangeRateList { get; set; }
    }
}

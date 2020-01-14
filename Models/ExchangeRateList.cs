using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ExchangeRateList
    {
        public int ID { get; set; }
        public int BankID { get; set; }

        public Bank Bank { get; set; }
        public ICollection<ExchangeRate> ExchangeRates { get; set; }

    }
}

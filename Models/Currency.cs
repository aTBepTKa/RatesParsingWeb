using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    // ISO 4217.
    public class Currency
    {
        public int ID { get; set; }
        public string CurrencyName { get; set; }
        public string TextCode { get; set; }
        public int NumCode { get; set; }

        public ICollection<Bank> Banks { get; set; }
    }
}

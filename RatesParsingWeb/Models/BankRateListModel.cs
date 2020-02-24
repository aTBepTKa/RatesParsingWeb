using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class BankRateListModel
    {
        public BankModel Bank { get; set; }
        
        public ExchangeRateListModel ExchangeRateList { get; set; }
    }
}

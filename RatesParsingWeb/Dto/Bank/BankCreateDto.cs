using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.Bank
{
    public class BankCreateDto : IBankValidity
    {
        public string SwiftCode { get; set; }
        public string Name { get; set; }
        public string BankUrl { get; set; }
        public string RatesUrl { get; set; }
        public int CurrencyId { get; set; }
        public ParsingSettingsDto ParsingSettings { get; set; }
    }
}

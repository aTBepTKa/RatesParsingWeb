using ParsingMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Dto.ParsingService
{
    public class ParsingRequest : IParsingRequest
    {
        public string TaskName { get; set; }
        public string RatesUrlPage { get; set; }

        public string TextCodeXpath { get; set; }
        public string UnitXpath { get; set; }
        public string ExchangeRateXpath { get; set; }
        public string VariablePartOfXpath { get; set; }

        public string NumberDecimalSeparator { get; set; }
        public string NumberGroupSeparator { get; set; }

        public int StartXpathRow { get; set; }
        public int EndXpathRow { get; set; }

        public Dictionary<string, string[]> UnitScripts { get; set; }
        public Dictionary<string, string[]> TextCodeScripts { get; set; }
    }
}

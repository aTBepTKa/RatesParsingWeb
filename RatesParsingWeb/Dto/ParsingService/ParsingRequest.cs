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
        public string RatesUrl { get; set; }

        public string TextCodeXpath { get; set; }
        public string UnitXpath { get; set; }
        public string ExchangeRateXpath { get; set; }
        public string VariablePartOfXpath { get; set; }

        public string NumberDecimalSeparator { get; set; }
        public string NumberGroupSeparator { get; set; }

        public int StartXpathRow { get; set; }
        public int EndXpathRow { get; set; }

        public Dictionary<string, string[]> UnitCommands { get; set; }
        public Dictionary<string, string[]> TextCodeCommands { get; set; }
    }
}

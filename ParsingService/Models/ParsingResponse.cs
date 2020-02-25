using ParsingMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    class ParsingResponse : IParsingResponse
    {
        public ParsingResponse()
        {

        }
        public ParsingResponse(IEnumerable<IExchangeRate> rates)
        {
            ExchangeRates = rates;
        }
        public IEnumerable<IExchangeRate> ExchangeRates { get; set; }
    }
}

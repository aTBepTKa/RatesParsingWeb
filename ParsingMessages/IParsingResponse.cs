using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages
{
    public interface IParsingResponse
    {
        IEnumerable<IExchangeRate> ExchangeRates { get; set; }
    }
}

using ParsingMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Models
{
    /// <summary>
    /// Ответ на запрос. Возвращает коллекцию обменных курсов по банку.
    /// </summary>
    class ParsingResponse : IParsingResponse
    {
        public IEnumerable<IExchangeRate> ExchangeRates { get; set; }
        public bool IsSuccesfullParsed { get; set; }
        public string ErrorDescription { get; set; }
    }
}

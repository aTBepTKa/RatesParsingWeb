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
        public ParsingResponse(IEnumerable<IExchangeRate> exchangeRates,
                               bool isSuccessfullParsed,
                               Dictionary<string, IEnumerable<string>> errorDictionary)
        {
            ExchangeRates = exchangeRates;
            IsSuccefullParsed = isSuccessfullParsed;
            ErrorDictionary = errorDictionary;
        }

        public IEnumerable<IExchangeRate> ExchangeRates { get; set; }
        public bool IsSuccefullParsed { get; set; }
        public Dictionary<string, IEnumerable<string>> ErrorDictionary { get; set; }
    }
}

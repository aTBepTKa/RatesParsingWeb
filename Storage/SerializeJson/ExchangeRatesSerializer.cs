using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using RatesParsingWeb.Storage.SerializeJson.Json;
using Mapster;

namespace RatesParsingWeb.Storage.SerializeJson
{
    /// <summary>
    /// Представляет средства для десериализации обменных курсов.
    /// </summary>
    public class ExchangeRatesSerializer
    {
        public ExchangeRatesSerializer(IEnumerable<Bank> banks, IEnumerable<Currency> currencies)
        {
            this.banks = banks;
            this.currencies = currencies;
        }

        private readonly IEnumerable<Bank> banks;
        private readonly IEnumerable<Currency> currencies;

        public IEnumerable<ExchangeRateList> GetExchangeRateLists()
        {            
            var banksJson = JsonSerializer.Deserialize<IEnumerable<BankJson>>(Properties.Resources.FakeExchangeRates);
            
            var exchangeRateLists = new List<ExchangeRateList>();
            int rateId = 1;
            int listId = 1;

            foreach (var bankJson in banksJson)
            {
                int bankId = banks.FirstOrDefault(i => i.SwiftCode == bankJson.SwiftCode).Id;
                var lists = GetExchangeRateListsForBank(bankJson.ExchangeRateLists, bankId, ref listId, ref rateId);
                exchangeRateLists.AddRange(lists);
            }
            return exchangeRateLists;
        }

        private ICollection<ExchangeRateList> GetExchangeRateListsForBank(IEnumerable<ExchangeRateListJson> exchangeRateListJsons,
                                                                   int bankId,
                                                                   ref int listId,
                                                                   ref int rateId)
        {
            var exchangeRateLists = new List<ExchangeRateList>(exchangeRateListJsons.Count());
            foreach (var listJson in exchangeRateListJsons)
            {
                var list = new ExchangeRateList()
                {
                    Id = listId,
                    BankId = bankId,
                    DateTimeStamp = listJson.DateTimeStamp,
                    ExchangeRates = GetExchangeRates(listJson.ExchangeRates, listId, ref rateId)
                };
                exchangeRateLists.Add(list);
                listId++;
            }
            return exchangeRateLists;
        }

        private ICollection<ExchangeRate> GetExchangeRates(ICollection<ExchangeRateJson> exchangeRateJsons, int listId, ref int rateId)
        {
            var exchangeRates = new List<ExchangeRate>(exchangeRateJsons.Count());
            foreach (var rateJson in exchangeRateJsons)
            {
                var rate = new ExchangeRate()
                {
                    Id = rateId++,
                    CurrencyId = currencies.FirstOrDefault(i => i.TextCode == rateJson.TextCode).Id,
                    ExchangeRateListId = listId,
                    ExchangeRateValue = rateJson.ExchangeRateValue,
                    Unit = rateJson.Unit
                };
                exchangeRates.Add(rate);
            }
            return exchangeRates;
        }
    }
}

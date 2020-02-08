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

        /// <summary>
        /// Получить списки обменных курсов валют.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ExchangeRateList> GetExchangeRateLists()
        {            
            var banksJson = JsonSerializer.Deserialize<IEnumerable<BankJson>>(Properties.Resources.FakeExchangeRates);
            
            var exchangeRateLists = new List<ExchangeRateList>();
            int exchangeRateId = 1;
            int exchangeRateListId = 1;

            foreach (var bankJson in banksJson)
            {
                int bankId = banks.FirstOrDefault(i => i.SwiftCode == bankJson.SwiftCode).Id;
                var lists = GetExchangeRateListsForBank(bankJson.ExchangeRateLists, bankId, ref exchangeRateListId, ref exchangeRateId);
                exchangeRateLists.AddRange(lists);
            }
            return exchangeRateLists;
        }

        /// <summary>
        /// Получить списки обменных курсов.
        /// </summary>
        /// <param name="listsJsons">Списки курсов из файла .json.</param>
        /// <param name="bankId">Id банка.</param>
        /// <param name="exchangeRateListId">Id списка.</param>
        /// <param name="exchangeRateId">Id курса.</param>
        /// <returns></returns>
        private ICollection<ExchangeRateList> GetExchangeRateListsForBank(IEnumerable<ExchangeRateListJson> listsJsons,
                                                                   int bankId,
                                                                   ref int exchangeRateListId,
                                                                   ref int exchangeRateId)
        {
            var exchangeRateLists = new List<ExchangeRateList>(listsJsons.Count());
            foreach (var listJson in listsJsons)
            {
                var list = new ExchangeRateList()
                {
                    Id = exchangeRateListId,
                    BankId = bankId,
                    DateTimeStamp = listJson.DateTimeStamp,
                    ExchangeRates = GetExchangeRates(listJson.ExchangeRates, exchangeRateListId, ref exchangeRateId)
                };
                exchangeRateLists.Add(list);
                exchangeRateListId++;
            }
            return exchangeRateLists;
        }

        /// <summary>
        /// Получить последовательность обменных курсов.
        /// </summary>
        /// <param name="ratesJsons">Курсы из файла .json.</param>
        /// <param name="exchangeRateListId">Id списка.</param>
        /// <param name="exchangeRateId">Id курса.</param>
        /// <returns></returns>
        private ICollection<ExchangeRate> GetExchangeRates(ICollection<ExchangeRateJson> ratesJsons, int exchangeRateListId, ref int exchangeRateId)
        {
            var exchangeRates = new List<ExchangeRate>(ratesJsons.Count);
            foreach (var rateJson in ratesJsons)
            {
                var rate = new ExchangeRate()
                {
                    Id = exchangeRateId++,
                    CurrencyId = currencies.FirstOrDefault(i => i.TextCode == rateJson.TextCode).Id,
                    ExchangeRateListId = exchangeRateListId,
                    ExchangeRateValue = rateJson.ExchangeRateValue,
                    Unit = rateJson.Unit
                };
                exchangeRates.Add(rate);
            }
            return exchangeRates;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.SerializeJson;
using RatesParsingWeb.Storage.SerializeXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.DataSeeding
{
    public class SeedBank
    {
        public SeedBank(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        private readonly ModelBuilder modelBuilder;
        private IEnumerable<Currency> Currencies;
        private IEnumerable<Bank> Banks;

        public IEnumerable<Bank> SeedAll()
        {
            SeedCurrencies();
            SeedBanks();
            SeedExchangeRates();
            return Banks;
        }

        /// <summary>
        /// Заполнить таблицу Currencies.
        /// </summary>
        private void SeedCurrencies()
        {
            var currencySerializer = new CurrencySerializer();
            IEnumerable<CurrencyXmlItem> currenciesXml = currencySerializer.GetCurrenciesFromXml();
            var currencies = new List<Currency>(currenciesXml.Count());
            foreach (var cur in currenciesXml)
            {
                var newCurrency = new Currency();
                if (int.TryParse(cur.CcyNbr, out int numCodeTemp))
                {
                    newCurrency.Id = numCodeTemp;
                    newCurrency.Name = cur.CcyNm;
                    newCurrency.TextCode = cur.Ccy;
                    currencies.Add(newCurrency);
                }
            }
            Currencies = currencies;
            modelBuilder.Entity<Currency>().HasData(currencies);
        }

        /// <summary>
        /// Заполнить таблицу Banks.
        /// </summary>
        private void SeedBanks()
        {
            int bankId = 1;
            Bank[] banks = new Bank[]
            {
                new Bank
                {
                    Id = bankId++,
                    SwiftCode = "BNLNGE22XXX",
                    Name = "National Bank of Georgia",
                    CurrencyId = Currencies.Single(i=>i.TextCode == "GEL").Id,
                    BankUrl = "https://www.nbg.gov.ge",
                    RatesUrl = "https://www.nbg.gov.ge/index.php?m=582&lng=eng"
                },
                new Bank
                {
                    Id = bankId++,
                    SwiftCode = "NBPLPLPWBAN",
                    Name = "National Bank of Poland",
                    CurrencyId = Currencies.Single(i=>i.TextCode == "PLN").Id,
                    BankUrl = "https://www.nbp.pl",
                    RatesUrl = "https://www.nbp.pl/homen.aspx?f=/kursy/RatesA.html"
                },
                new Bank
                {
                    Id = bankId++,
                    SwiftCode = "CBRFRUMMXXX",
                    Name = "The Central Bank of the Russian Federation",
                    CurrencyId = Currencies.Single(i=>i.TextCode == "RUB").Id,
                    BankUrl = "https://www.cbr.ru",
                    RatesUrl = "https://www.cbr.ru/eng/currency_base/daily/"
                },
                new Bank
                {
                    Id = bankId++,
                    SwiftCode = "ECBFDEFFEUM",
                    Name = "European Central Bank",
                    CurrencyId = Currencies.Single(i=>i.TextCode == "EUR").Id,
                    BankUrl = "https://www.ecb.europa.eu",
                    RatesUrl = "https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html"
                }
            };
            Banks = banks;
            modelBuilder.Entity<Bank>().HasData(banks);
        }

        /// <summary>
        /// Заполнить таблицы ExchangeRateLists и ExchangeRates
        /// </summary>
        private void SeedExchangeRates()
        {
            // Получить обменные курсы валют по спискам и заполнить таблицы списков и курсов.
            var serializer = new ExchangeRatesSerializer(Banks, Currencies);
            IEnumerable<ExchangeRateList> lists = serializer.GetExchangeRateLists();
            modelBuilder.Entity<ExchangeRateList>().HasData(lists.Select(i => new ExchangeRateList()
            {
                Id = i.Id,
                BankId = i.BankId,
                DateTimeStamp = i.DateTimeStamp
            }));
            modelBuilder.Entity<ExchangeRate>().HasData(lists.SelectMany(i => i.ExchangeRates));
        }
    }
}

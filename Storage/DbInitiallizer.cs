using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.SerializeXml;

namespace RatesParsingWeb.Storage
{
    // Пусть пока побудет. А вообще УДОЛИТЬ!

    /// <summary>
    /// Заполняет базу данных тестовыми данными.
    /// </summary>
    public static class DbInitiallizer
    {
        /// <summary>
        /// Заполнить базу данных тестовыми данными.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public static void Initialize(BankRatesContext context)
        {
            // Заполнить таблицу Currencies.
            var currencySerializer = new CurrencySerializer();
            IEnumerable<CurrencyXmlItem> currenciesXml = currencySerializer.GetCurrenciesFromXml();
            var currencies = new List<Currency>(currenciesXml.Count());
            foreach (var cur in currenciesXml)
            {
                var newCurrency = new Currency
                {
                    Name = cur.CcyNm,
                    TextCode = cur.Ccy,
                };
                if (int.TryParse(cur.CcyNbr, out int numCodeTemp))
                {
                    newCurrency.NumCode = numCodeTemp;
                    currencies.Add(newCurrency);
                }
            }
            context.Currencies.AddRange(currencies);
            context.SaveChanges();

            // Заполнить таблицу Banks.
            var banks = new Bank[]
            {
                new Bank
                {
                    Name = "National Bank of Georgia",
                    Currency = context.Currencies.Where(i=>i.TextCode=="GEL").FirstOrDefault(),
                    RatesUrl = "https://www.nbg.gov.ge/index.php?m=582&lng=eng"
                },
                new Bank
                {
                    Name = "National Bank of Poland",
                    Currency = context.Currencies.Where(i=>i.TextCode=="PLN").FirstOrDefault(),
                    RatesUrl = "https://www.nbp.pl/homen.aspx?f=/kursy/RatesA.html"
                }
            };
            context.AddRange(banks);
            context.SaveChanges();
        }
    }
}

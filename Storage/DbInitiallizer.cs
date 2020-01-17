using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.SerializeXml;

namespace RatesParsingWeb.Storage
{
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
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Заполнить таблицу Currencies.
            var currencySerializer = new CurrencySerializer();
            IEnumerable<CurrencyXmlItem> currenciesXml = currencySerializer.GetCurrenciesFromXml();
            var currencies = new List<Currency>(currenciesXml.Count());
            foreach (var cur in currenciesXml)
            {
                var newCurrency = new Currency
                {
                    CurrencyName = cur.CcyNm,
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
                new Bank{BankName = "TestName"}
            };
            foreach (var bank in banks)
            {
                context.Banks.Add(bank);
            }
            context.SaveChanges();
        }
    }
}

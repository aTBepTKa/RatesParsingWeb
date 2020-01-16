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

            // Заполнить табдицу Currencies.
            var currencySerializer = new CurrencySerializer();
            IEnumerable<Currency> currencies = currencySerializer.GetCurrenciesFromXml();
            foreach(var curr in currencies)
            {
                context.Currencies.Add(curr);
            }
            context.SaveChanges();
        }
    }
}

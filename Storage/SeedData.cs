using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.SerializeXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage
{
    /// <summary>
    /// Представляет средства для заполнения базы данных начальными данными.
    /// </summary>
    public class SeedData
    {
        public SeedData(ModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }

        private readonly ModelBuilder ModelBuilder;

        private IEnumerable<Currency> Currencies { get; set; }
        private IEnumerable<Bank> Banks { get; set; }

        /// <summary>
        /// Заполнить базу данных.
        /// </summary>
        public void SeedAll()
        {
            SeedCurrencies();
            SeedBanks();
            SeedParsingSettings();
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
            ModelBuilder.Entity<Currency>().HasData(currencies);
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
            ModelBuilder.Entity<Bank>().HasData(banks);
        }

        /// <summary>
        /// Заполнить таблицу ParsingSettings.
        /// </summary>
        private void SeedParsingSettings()
        {
            int settingsId = 1;
            ParsingSettings[] settings = new ParsingSettings[]
            {
                new ParsingSettings
                {
                    Id = settingsId++,
                    BankId = Banks.Single(i=>i.SwiftCode == "BNLNGE22XXX").Id,
                    NumberDecimalSeparator = '.',
                    NumberGroupSeparator = ',',
                    StartXpathRow = 1,
                    EndXpathRow = 43,
                    VariablePartOfXpath = "$VARIABLE",
                    TextCodeXpath = "//*[@id='currency_id']/table/tr[$VARIABLE]/td[1]",
                    UnitXpath = "//*[@id='currency_id']/table/tr[$VARIABLE]/td[2]",
                    ExchangeRateXpath = "//*[@id='currency_id']/table/tr[$VARIABLE]/td[3]"
                },
                new ParsingSettings
                {
                    Id = settingsId++,
                    BankId = Banks.Single(i=>i.SwiftCode == "NBPLPLPWBAN").Id,
                    NumberDecimalSeparator = '.',
                    NumberGroupSeparator = ',',
                    StartXpathRow = 2,
                    EndXpathRow = 36,
                    VariablePartOfXpath = "$VARIABLE",
                    TextCodeXpath = "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[2]",
                    UnitXpath = "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[2]",
                    ExchangeRateXpath = "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[3]"
                },
                new ParsingSettings
                {
                    Id = settingsId++,
                    BankId = Banks.Single(i=>i.SwiftCode == "CBRFRUMMXXX").Id,
                    NumberDecimalSeparator = '.',
                    NumberGroupSeparator = ',',
                    StartXpathRow = 2,
                    EndXpathRow = 35,
                    VariablePartOfXpath = "$VARIABLE",
                    TextCodeXpath = "//*[@id='content']/table/tbody/tr[$VARIABLE]/td[2]",
                    UnitXpath = "//*[@id='content']/table/tbody/tr[$VARIABLE]/td[3]",
                    ExchangeRateXpath = "//*[@id='content']/table/tbody/tr[$VARIABLE]/td[5]"
                },
                new ParsingSettings
                {
                    Id = settingsId++,
                    BankId = Banks.Single(i=>i.SwiftCode == "ECBFDEFFEUM").Id,
                    NumberDecimalSeparator = '.',
                    NumberGroupSeparator = ',',
                    StartXpathRow = 1,
                    EndXpathRow = 32,
                    VariablePartOfXpath = "$VARIABLE",
                    TextCodeXpath = "//*[@id='ecb-content-col']/main/div/table/tbody/tr[$VARIABLE]/td[1]",
                    UnitXpath = "//*[@id='ecb-content-col']/main/div/table/tbody/tr[$VARIABLE]/td[1]",
                    ExchangeRateXpath = "//*[@id='ecb-content-col']/main/div/table/tbody/tr[$VARIABLE]/td[3]"
                },
            };
            ModelBuilder.Entity<ParsingSettings>().HasData(settings);
        }
    }
}

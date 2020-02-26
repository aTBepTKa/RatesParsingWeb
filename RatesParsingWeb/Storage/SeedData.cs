using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.SerializeJson;
using RatesParsingWeb.Storage.SerializeXml;
using System.Collections.Generic;
using System.Linq;

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

        private IEnumerable<ParsingSettings> ParsingSettings { get; set; }
        private IEnumerable<Command> Commands { get; set; }


        /// <summary>
        /// Заполнить базу данных.
        /// </summary>
        public void SeedAll()
        {
            SeedCurrencies();
            SeedBanks();
            SeedParsingSettings();
            SeedExchangeRates();
            SeedCommands();
            SeedCommandAssignment();
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
            ParsingSettings = settings;
            ModelBuilder.Entity<ParsingSettings>().HasData(settings);
        }

        /// <summary>
        /// Заполнить таблицы ExchangeRateLists и ExchangeRates
        /// </summary>
        private void SeedExchangeRates()
        {
            // Получить обменные курсы валют по спискам и заполнить таблицы списков и курсов.
            var serializer = new ExchangeRatesSerializer(Banks, Currencies);
            IEnumerable<ExchangeRateList> lists = serializer.GetExchangeRateLists();
            ModelBuilder.Entity<ExchangeRateList>().HasData(lists.Select(i => new ExchangeRateList()
            {
                Id = i.Id,
                BankId = i.BankId,
                DateTimeStamp = i.DateTimeStamp
            }));
            ModelBuilder.Entity<ExchangeRate>().HasData(lists.SelectMany(i => i.ExchangeRates));
        }

        /// <summary>
        /// Заполнить таблицу Commands.
        /// </summary>
        private void SeedCommands()
        {
            int scriptId = 1;
            int parameterId = 1;
            var commands = new Command[]
            {
                new Command
                {
                    Id = scriptId++,
                    Name = "GetNumberFromText",
                    Description = "Возвращает числа из текстовой строки"
                },
                new Command
                {
                    Id = scriptId++,
                    Name = "GetTextFromEnd",
                    Description = "Возвращает строку заданной длины начиная начиная с конца исходной строки"
                },
                new Command
                {
                    Id = scriptId++,
                    Name = "ReplaceSubString",
                    Description = "Находит строку и заменяет новой"
                }
            };
            Commands = commands;
            ModelBuilder.Entity<Command>().HasData(commands);

            var commandParameters = new CommandParameter[]
            {
                new CommandParameter
                {
                    Id = parameterId++,
                    CommandId = commands.Single(i=>i.Name == "GetTextFromEnd").Id,
                    Name = "Length",
                    Description = "Длина строки"
                },
                new CommandParameter
                {
                    Id = parameterId++,
                    CommandId = commands.Single(i=>i.Name == "ReplaceSubString").Id,
                    Name = "OldString",
                    Description = "Исходная строка"
                },
                new CommandParameter
                {
                    Id = parameterId++,
                    CommandId = commands.Single(i=>i.Name == "ReplaceSubString").Id,
                    Name = "NewString",
                    Description = "Новая строка"
                }
            };
            ModelBuilder.Entity<CommandParameter>().HasData(commandParameters);
        }

        /// <summary>
        /// Заполнить таблицы CommandAssignment.
        /// </summary>
        private void SeedCommandAssignment()
        {
            // Задать команды для National Bank of Poland.
            // Обработка текстового кода валюты.
            var textAssignmentId = 1;
            var textAssignment = new TextCodeCommandAssignment
            {
                Id = textAssignmentId++,
                ParsingSettingsId = ParsingSettings.Single(p =>
                    p.BankId == Banks.Single(b => b.SwiftCode == "NBPLPLPWBAN").Id).Id,
                CommandId = Commands.Single(c => c.Name == "GetTextFromEnd").Id
            };
            ModelBuilder.Entity<TextCodeCommandAssignment>().HasData(textAssignment);

            var textParameterId = 1;
            var textParameters = new TextCodeCommandParameter
            {
                Id = textParameterId++,
                TextCodeCommandAssignmentId = 1,
                Value = "3"
            };
            ModelBuilder.Entity<TextCodeCommandParameter>().HasData(textParameters);


            // Обработка единицы измерения валюты.
            var unitAssignmentId = 1;
            var unitAssignment = new UnitCommandAssignment
            {
                Id = unitAssignmentId++,
                ParsingSettingsId = ParsingSettings.Single(p =>
                    p.BankId == Banks.Single(b => b.SwiftCode == "NBPLPLPWBAN").Id).Id,
                CommandId = Commands.Single(c => c.Name == "GetNumberFromText").Id
            };
            ModelBuilder.Entity<UnitCommandAssignment>().HasData(unitAssignment);
        }
    }
}

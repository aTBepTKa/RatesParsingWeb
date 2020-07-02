using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.DataSeeding
{
    public class SeedParsingSettings
    {
        public SeedParsingSettings(ModelBuilder modelBuilder, IEnumerable<Bank> banks)
        {
            ModelBuilder = modelBuilder;
            Banks = banks;
        }

        private readonly ModelBuilder ModelBuilder;
        private readonly IEnumerable<Bank> Banks;

        private IEnumerable<ParsingSettings> ParsingSettings { get; set; }
        private IEnumerable<Command> Commands { get; set; }
        private IEnumerable<CommandParameter> CommandParameters { get; set; }
        private IEnumerable<CommandFieldName> CommandFieldNames { get; set; }

        public void SeedAll()
        {
            SeedSettings();
            SeedCommandFieldName();
            SeedCommands();
        }

        /// <summary>
        /// Заполнить таблицу ParsingSettings.
        /// </summary>
        private void SeedSettings()
        {
            int settingsId = 1;
            ParsingSettings = new ParsingSettings[]
            {
                new ParsingSettings
                {
                    Id = settingsId++,
                    RatesUrl = "https://www.nbg.gov.ge/index.php?m=582&lng=eng",
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
                    RatesUrl = "https://www.nbp.pl/homen.aspx?f=/kursy/RatesA.html",
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
                    RatesUrl = "https://www.cbr.ru/eng/currency_base/daily/",
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
                    RatesUrl = "https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html",
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
            ModelBuilder.Entity<ParsingSettings>().HasData(ParsingSettings);
        }

        /// <summary>
        /// Заполнить наименования полей для применения команды обработки текста.
        /// </summary>
        public void SeedCommandFieldName()
        {
            var assignmentId = 1;
            CommandFieldNames = new CommandFieldName[]
            {
                new CommandFieldName
                {
                    Id = assignmentId++,
                    Name="TextCode"
                },
                new CommandFieldName
                {
                    Id=assignmentId++,
                    Name="Unit"
                },
                new CommandFieldName
                {
                    Id = assignmentId,
                    Name = "ExchangeRate"
                }
            };
            ModelBuilder.Entity<CommandFieldName>().HasData(CommandFieldNames);
        }
        /// <summary>
        /// Заполнить таблицу Commands.
        /// </summary>
        private void SeedCommands()
        {
            int commandId = 1;
            int parameterId = 1;
            Commands = new Command[]
            {
                new Command
                {
                    Id = commandId++,
                    ParsingSettingsId = ParsingSettings.Single(p =>
                        p.BankId == Banks.Single(b => b.SwiftCode == "NBPLPLPWBAN").Id).Id,
                    CommandFieldNameId = CommandFieldNames.Single(i=>i.Name == "TextCode").Id,
                    Name = "GetTextFromEnd",
                    Description = "Возвращает строку заданной длины начиная начиная с конца исходной строки",
                },
                new Command
                {
                    Id = commandId++,
                    ParsingSettingsId = ParsingSettings.Single(p =>
                        p.BankId == Banks.Single(b => b.SwiftCode == "NBPLPLPWBAN").Id).Id,
                    CommandFieldNameId = CommandFieldNames.Single(i=>i.Name == "Unit").Id,
                    Name = "GetNumberFromText",
                    Description = "Возвращает числа из текстовой строки",
                }
            };
            ModelBuilder.Entity<Command>().HasData(Commands);

            CommandParameters = new CommandParameter[]
            {
                new CommandParameter
                {
                    Id = parameterId++,
                    CommandId = Commands
                        .Where(i=>i.ParsingSettingsId == ParsingSettings
                            .Single(p => p.BankId == Banks
                                .Single(b => b.SwiftCode == "NBPLPLPWBAN").Id).Id)
                        .Single(i=>i.Name == "GetTextFromEnd").Id,
                    Name = "Length",
                    Description = "Длина строки",
                    Value = "3"
                }
            };
            ModelBuilder.Entity<CommandParameter>().HasData(CommandParameters);
        }
    }
}

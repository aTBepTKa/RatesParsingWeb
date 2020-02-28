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
        private IEnumerable<CommandParameterValue> CommandParameterValues { get; set; }
        private IEnumerable<AssignmentFieldName> AssignmentFieldNames { get; set; }
        private IEnumerable<CommandAssignment> CommandAssignments { get; set; }

        public void SeedAll()
        {
            SeedSettings();
            SeedCommands();
            SeedAssignmentFieldName();
            SeedCommandAssignment();
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
            ModelBuilder.Entity<ParsingSettings>().HasData(ParsingSettings);
        }

        /// <summary>
        /// Заполнить таблицу Commands.
        /// </summary>
        private void SeedCommands()
        {
            int scriptId = 1;
            int parameterId = 1;
            Commands = new Command[]
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
            ModelBuilder.Entity<Command>().HasData(Commands);

            CommandParameters = new CommandParameter[]
            {
                new CommandParameter
                {
                    Id = parameterId++,
                    CommandId = Commands.Single(i=>i.Name == "GetTextFromEnd").Id,
                    Name = "Length",
                    Description = "Длина строки"
                },
                new CommandParameter
                {
                    Id = parameterId++,
                    CommandId = Commands.Single(i=>i.Name == "ReplaceSubString").Id,
                    Name = "OldString",
                    Description = "Исходная строка"
                },
                new CommandParameter
                {
                    Id = parameterId++,
                    CommandId = Commands.Single(i=>i.Name == "ReplaceSubString").Id,
                    Name = "NewString",
                    Description = "Новая строка"
                }
            };
            ModelBuilder.Entity<CommandParameter>().HasData(CommandParameters);
        }

        /// <summary>
        /// Заполнить наименования полей для применения команды обработки текста.
        /// </summary>
        public void SeedAssignmentFieldName()
        {
            var assignmentId = 1;
            AssignmentFieldNames = new AssignmentFieldName[]
            {
                new AssignmentFieldName
                {
                    Id = assignmentId++,
                    Name="TextCode"
                },
                new AssignmentFieldName
                {
                    Id=assignmentId++,
                    Name="Unit"
                },
                new AssignmentFieldName
                {
                    Id = assignmentId,
                    Name = "ExchangeRate"
                }
            };
            ModelBuilder.Entity<AssignmentFieldName>().HasData(AssignmentFieldNames);
        }

        /// <summary>
        /// Заполнить таблицы CommandAssignment.
        /// </summary>
        private void SeedCommandAssignment()
        {
            // Задать команды для National Bank of Poland.
            // Обработка текстового кода валюты.
            var commandAssignmentId = 1;
            CommandAssignments = new CommandAssignment[]
            {
                new CommandAssignment
                {
                    Id = commandAssignmentId++,
                    AssignmentFieldNameId = AssignmentFieldNames.Single(i=>i.Name == "TextCode").Id,
                    ParsingSettingsId = ParsingSettings.Single(p =>
                        p.BankId == Banks.Single(b => b.SwiftCode == "NBPLPLPWBAN").Id).Id,
                    CommandId = Commands.Single(c => c.Name == "GetTextFromEnd").Id
                },
                new CommandAssignment
                {
                    Id = commandAssignmentId++,
                    AssignmentFieldNameId = AssignmentFieldNames.Single(i=>i.Name == "Unit").Id,
                    ParsingSettingsId = ParsingSettings.Single(p =>
                        p.BankId == Banks.Single(b => b.SwiftCode == "NBPLPLPWBAN").Id).Id,
                    CommandId = Commands.Single(c => c.Name == "GetNumberFromText").Id
                }
            };
            ModelBuilder.Entity<CommandAssignment>().HasData(CommandAssignments);

            var commandParameterValueId = 1;
            CommandParameterValues = new CommandParameterValue[]
            {
                new CommandParameterValue
                {
                    Id = commandParameterValueId++,
                    CommandAssignmentId = 1,
                    CommandParameterId = CommandParameters.Single(i=>i.Name == "Length").Id,
                    Value = "3"
                }
            };
            ModelBuilder.Entity<CommandParameterValue>().HasData(CommandParameterValues);
        }
    }
}

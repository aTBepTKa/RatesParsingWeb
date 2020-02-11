using System.Collections.Generic;
using System.Text.Json;

namespace RatesParsingWeb.Dto.ExternalCommand
{
    /// <summary>
    /// Временные средства для получения списка команд из стороннего приложения.
    /// </summary>
    public class FakeTaxi
    {
        /// <summary>
        /// Получить список команд.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ExternalCommandDto> GetCommands()
        {
            var commandsJson = GetCommandsListFromConsoleApp();
            var commands = JsonSerializer.Deserialize<IEnumerable<ExternalCommandDto>>(commandsJson);
            return commands;
        }

        /// <summary>
        /// Сериализовать список команд.
        /// </summary>
        /// <returns></returns>
        private string GetCommandsListFromConsoleApp()
        {
            var commandsClass = GenerateCommandsList();
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var commandsJson = JsonSerializer.Serialize(commandsClass, options);
            return commandsJson;
        }

        /// <summary>
        /// Сформировать список команд.
        /// </summary>
        private IEnumerable<ExternalCommandDto> GenerateCommandsList()
        {
            var commands = new ExternalCommandDto[]
            {
                new ExternalCommandDto()
                {
                    Name = "GetStringFromStart",
                    Description = "Получить строку заданной длины с начала исходной строки.",
                    CommandParameters = new ExternalCommandParameterDto[]
                    {
                        new ExternalCommandParameterDto()
                        {
                            Name = "Length",
                            Description = "Длина строки."
                        }
                    }
                },
                new ExternalCommandDto()
                {
                    Name = "GetStringFromPosition",
                    Description = "Получить строку заданной длины с заданной позиции.",
                    CommandParameters = new ExternalCommandParameterDto[]
                    {
                        new ExternalCommandParameterDto()
                        {
                            Name = "Position",
                            Description = "Начальная позиция получаемой строки."
                        },
                        new ExternalCommandParameterDto()
                        {
                            Name = "Length",
                            Description = "Длина получаемой строки."
                        }
                    }
                },
                new ExternalCommandDto()
                {
                    Name = "GetPinkElephants",
                    Description = "Получить розовых слоников.",
                    CommandParameters = new ExternalCommandParameterDto[]
                    {
                        new ExternalCommandParameterDto()
                        {
                            Name = "Count",
                            Description = "Количество слоников."
                        },
                        new ExternalCommandParameterDto()
                        {
                            Name = "Size",
                            Description = "Размер слоников."
                        },
                        new ExternalCommandParameterDto()
                        {
                            Name = "Age",
                            Description = "Возраст слоников."
                        }
                    }
                }
            };
            return commands;
        }
    }
}

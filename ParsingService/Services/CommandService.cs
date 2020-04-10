using ParsingService.Models;
using System;
using System.Collections.Generic;

namespace ParsingService.Services
{
    /// <summary>
    /// Предоставляет список команд для обработки строк.
    /// </summary>
    class CommandService
    {
        public CommandListResult GetCommandsResponse()
        {
            var result = new CommandListResult();
            try
            {
                result.Commands = GetCommands();
            }
            catch (Exception ex)
            {
                result.SetError($"Ошибка при получении списка команд: {ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// Получить список команд для обработки строк.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Command> GetCommands()
        {
            Type methodsType = typeof(Commands);
            var methods = methodsType.GetMethods();
            var commands = new List<Command>(methods.Length);

            foreach (var method in methods)
            {
                var commandAttributes = (CommandAttribute[])method.GetCustomAttributes(typeof(CommandAttribute), false);
                if (commandAttributes == null || commandAttributes.Length < 1)
                    continue;
                var commandAttribute = commandAttributes[0];
                var command = new Command()
                {
                    Name = commandAttribute.Name,
                    Description = commandAttribute.Description
                };

                var parameterAttributes = (ParameterAttribute[])method.GetCustomAttributes(typeof(ParameterAttribute), false);
                var commandParameters = new List<CommandParameter>(parameterAttributes.Length);
                foreach (var parameterAttribute in parameterAttributes)
                {
                    var commandParameter = new CommandParameter()
                    {
                        Name = parameterAttribute.Name,
                        // TODO: Прикрутить описание параметра команды.
                        Description = parameterAttribute.Description
                    };
                    commandParameters.Add(commandParameter);
                }
                command.CommandParameters = commandParameters;
                commands.Add(command);
            }
            return commands;
        }
    }
}

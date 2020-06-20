using ParsingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                result.Message = GetCommands();
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
            var commands = typeof(Commands)
                .GetMethods()
                .Where(x => x.CustomAttributes.Any(c => c.AttributeType == typeof(CommandAttribute)))
                .Select(method =>
                {
                    var commandAttributes = (CommandAttribute[])method.GetCustomAttributes(typeof(CommandAttribute), false);
                    var commandAttribute = commandAttributes[0];
                    var parameterAttributes = (ParameterAttribute[])method.GetCustomAttributes(typeof(ParameterAttribute), false);
                    return new Command
                    {
                        Name = commandAttribute.Name,
                        Description = commandAttribute.Description,
                        CommandParameters = parameterAttributes.Select(x => new CommandParameter
                        {
                            Name = x.Name,
                            Description = x.Description
                        })
                    };
                }).ToArray();
            return commands;
        }
    }
}

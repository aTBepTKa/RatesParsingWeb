using Mapster;
using MassTransit;
using ParsingMessages.Command;
using ParsingService.Services;
using System.Threading.Tasks;

namespace ParsingService.Consumers
{
    /// <summary>
    /// Средства для формирования списка команд.
    /// </summary>
    public class CommandConsumer : IConsumer<ICommandRequest>
    {
        public async Task Consume(ConsumeContext<ICommandRequest> context)
        {
            ConsoleLog.ShowMessage($"Получен запрос на получение списка команд: {context.Message.TaskName}.");
            ICommandResponse response = CommandService.GetCommandsResponse();
            await context.RespondAsync(response);
            ConsoleLog.ShowMessage($"Список команд по запросу '{context.Message.TaskName}' отправлен клиенту.");
        }
    }
}

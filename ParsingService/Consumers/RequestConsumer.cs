using Mapster;
using MassTransit;
using ParsingMessages.Parsing;
using ParsingService.Models;
using ParsingService.Services;
using System.Threading.Tasks;

namespace ParsingService.Consumers
{
    /// <summary>
    /// Средства для парсинга сайта.
    /// </summary>
    public class RequestConsumer : IConsumer<IParsingRequest>
    {
        public async Task Consume(ConsumeContext<IParsingRequest> context)
        {
            ConsoleLog.ShowMessage($"Получено задание на парсинг: '{context.Message.TaskName}'.");

            IParsingResponse response = await ExchangeRatesService.GetBankRatesAsync(context.Message);
            await context.RespondAsync(response);

            ConsoleLog.ShowMessage($"Задание '{context.Message.TaskName}' выполнено. Ответ отправлен клиенту.");
        }
    }
}

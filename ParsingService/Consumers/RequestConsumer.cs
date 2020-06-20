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

            var request = context.Message.Adapt<ParsingRequest>();
            var response = await GetResponseAsync(request);
            await context.RespondAsync(response);

            ConsoleLog.ShowMessage($"Задание '{context.Message.TaskName}' выполнено. Ответ отправлен клиенту.");
        }

        private async Task<IParsingResponse> GetResponseAsync(ParsingRequest request)
        {
            var result = await ExchangeRatesService.GetBankRatesAsync(request);
            var response = result.Adapt<IParsingResponse>();
            return response;
        }
    }
}

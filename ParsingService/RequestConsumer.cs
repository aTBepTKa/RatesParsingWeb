using Mapster;
using MassTransit;
using ParsingMessages;
using ParsingService.Models;
using ParsingService.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsingService
{
    class RequestConsumer : IConsumer<IParsingRequest>
    {
        public async Task Consume(ConsumeContext<IParsingRequest> context)
        {
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Получено задание на парсинг: '{context.Message.TaskName}'.");

            var request = context.Message.Adapt<ParsingRequest>();
            var response = await GetResponse(request);
            await context.RespondAsync(response);

            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Задание '{context.Message.TaskName}' выполнено. Ответ отправлен клиенту.");
        }

        private async Task<IParsingResponse> GetResponse(ParsingRequest request)
        {
            var rateService = new ExchangeRatesService();
            var result = await rateService.GetBankRatesAsync(request);
            var response = result.Adapt<IParsingResponse>();
            return response;
        }
    }
}

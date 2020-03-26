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
            Console.WriteLine($"Получено задание на парсинг: {context.Message.TaskName}.");

            var request = context.Message.Adapt<BankRequest>();
            var response = await GetResponse(request);
            await context.RespondAsync(response);

            Console.WriteLine($"Задание '{context.Message.TaskName}' выполнено. Ответ отправлен клиенту.");
        }

        private async Task<IParsingResponse> GetResponse(BankRequest request)
        {
            var factory = new ExchangeRatesService();            
            var rates = await factory.GetBankRatesAsync(request);
            var ratesDto = rates.Adapt<IEnumerable<IExchangeRate>>();
            var response = new ParsingResponse(ratesDto, factory.IsSuccessfullParsed, factory.ErrorDictionary);
            return response;
        }
    }
}

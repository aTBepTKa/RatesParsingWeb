using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MassTransit;
using ParsingMessages;
using ParsingService.Models;

namespace ParsingService
{
    class RequestConsumer : IConsumer<IParsingRequest>
    {
        public async Task Consume(ConsumeContext<IParsingRequest> context)
        {
            Console.WriteLine($"Получено задание на парсинг: {context.Message.TaskName}.");

            var request = context.Message.Adapt<BankRequest>();
            var factory = new ExchangeRatesFactory();
            var rates = await factory.GetBankRatesAsync(request);
            var ratesDto = rates.Adapt<IEnumerable<IExchangeRate>>();
            var response = new ParsingResponse(ratesDto);

            await context.RespondAsync(response);
        }
    }
}

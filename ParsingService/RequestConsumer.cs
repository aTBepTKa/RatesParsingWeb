using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using ParsingMessages;

namespace ParsingService
{
    class RequestConsumer : IConsumer<IParsingRequest>
    {
        public async Task Consume(ConsumeContext<IParsingRequest> context)
        {
            Console.WriteLine($"Получено сообщение: {context.Message.Message}");
            await context.RespondAsync<IParsingResponse>(new ParsingResponse("Message from service."));
        }
    }

    class ParsingResponse : IParsingResponse
    {
        public ParsingResponse(string s)
        {
            Message = s;
        }
        public string Message { get; set; }
    }
}

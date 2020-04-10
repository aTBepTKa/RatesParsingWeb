using MassTransit;
using MassTransit.Util;
using ParsingService.Consumers;
using System;
using System.ServiceProcess;

namespace ParsingService
{
    class RequestService : ServiceBase
    {
        IBusControl _busControl;

        public void StartApp()
        {
            OnStart(null);
        }
        public void StopApp()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            var parsingQueue = "ParsingQueue";
            var commandQueue = "CommandQueue";
            ServiceName = "ParsingService";

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host("rabbitmq://localhost");
                cfg.ReceiveEndpoint(parsingQueue, e => e.Consumer<RequestConsumer>());
                cfg.ReceiveEndpoint(commandQueue, e => e.Consumer<CommandConsumer>());
            });
            TaskUtil.Await(() => _busControl.StartAsync());
            Console.WriteLine("Сервис запущен. Ожидаются входящие сообщения.");
        }

        protected override void OnStop()
        {
            _busControl?.Stop();
        }
    }
}

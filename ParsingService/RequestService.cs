using MassTransit;
using MassTransit.Util;
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
            ServiceName = "RequestService";

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host("rabbitmq://localhost");
                cfg.ReceiveEndpoint("ParsingQueue", e => e.Consumer<RequestConsumer>());
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

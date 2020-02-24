using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;

namespace ParsingService
{
    class RequestService : ServiceControl
    {
        IBusControl _busControl;

        public bool Start(HostControl hostControl)
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host("rabbitmq://localhost");
                cfg.ReceiveEndpoint("ParsingQueue", e => e.Consumer<RequestConsumer>());
            });

            TaskUtil.Await(() => _busControl.StartAsync());
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _busControl?.Stop();
            return true;
        }
    }
}

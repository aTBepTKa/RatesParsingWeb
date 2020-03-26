using ParsingService.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace ParsingService
{
    class Program
    {
        static void Main(string[] args)
        {
            // На вермя разработки отключаем возможность запуска программы как сервиса винды.
            //var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var isService = false;

            // Запустить приложение как сервис либо как консольное приложение.
            if (isService)
            {
                using var service = new RequestService();
                ServiceBase.Run(service);
            }
            else
            {
                Console.WriteLine("Запсук сервиса в консольном режиме...");
                var service = new RequestService();
                service.StartApp();
                Console.WriteLine("Нажмите любую клавишу для остановки...");
                Console.ReadKey(true);
                service.StopApp();
            }

        }
    }
}

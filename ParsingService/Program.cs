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
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

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

using System;
using Topshelf;

namespace ParsingService
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            return (int)HostFactory.Run(x => x.Service<RequestService>());
        }
    }
}

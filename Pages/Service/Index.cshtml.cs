using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatesParsingWeb.Models;
using RatesParsingWeb.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParsingMessages;
using System;

namespace RatesParsingWeb.Pages.Service
{
    public class IndexModel : PageModel
    {
        private readonly IBusControl busControl;
        public IndexModel(IBusControl bus)
        {
            busControl = bus;
        }
        public string ResponsedData { get; set; }
        public async Task OnGet()
        {
            var serviceAddress = new Uri("rabbitmq://localhost/ParsingQueue");
            IRequestClient<IParsingRequest, IParsingResponse> client =
                busControl.CreateRequestClient<IParsingRequest, IParsingResponse>(serviceAddress, TimeSpan.FromSeconds(10));
            var response =  await client.Request(new ParsingRequest("Welcome to Hell"));
            ResponsedData = response.Message;
        }
    }

    public class ParsingRequest : IParsingRequest
    {
        public ParsingRequest(string s)
        {
            Message = s;
        }
        public string Message { get; set; }
    }
}

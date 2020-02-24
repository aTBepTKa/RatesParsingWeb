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
        private readonly IRequestClient<IParsingRequest> requestClient;
        public IndexModel(IBusControl bus)
        {
            busControl = bus;
        }
        public string ResponsedData { get; set; }
        public async Task OnGet()        
        {
            var serviceAddress = new Uri("rabbitmq://localhost/ParsingQueue");
            var client = busControl.CreateRequestClient<IParsingRequest>(serviceAddress);
            var response = await client.GetResponse<IParsingResponse>(new ParsingRequest("OLOLO"));
            ResponsedData = response.Message.Message;

            //var responseMessage = await requestClient.GetResponse<IParsingResponse>(
            //    new ParsingRequest("Message from asp."));
            //ResponsedData = responseMessage.Message.Message;
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

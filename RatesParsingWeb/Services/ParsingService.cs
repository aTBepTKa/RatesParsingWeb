﻿using Mapster;
using MassTransit;
using ParsingMessages;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.ParsingService;
using RatesParsingWeb.Dto.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class ParsingService : IParsingService
    {
        private readonly IRequestClient<IParsingRequest> requestClient;
        private readonly ICurrencyService currencyService;

        public ParsingService(IBusControl bus, ICurrencyService currency)
        {
            var serviceAddress = new Uri("rabbitmq://localhost/ParsingQueue");
            requestClient = bus.CreateRequestClient<IParsingRequest>(serviceAddress);

            currencyService = currency;
        }

        public async Task<IEnumerable<ExchangeRateDto>> GetExchangeRates(ParsingSettingsDto parsingSettings, string taskName)
        {
            var request = GetRequest(parsingSettings);
            request.TaskName = taskName;
            var response = await requestClient.GetResponse<IParsingResponse>(request);
            var ratesResponse = response.Message.ExchangeRates;
            var rates = ratesResponse.Select(response =>
                new ExchangeRateDto
                {
                    Unit = response.Unit,
                    ExchangeRateValue = response.ExchangeRateValue,
                    Currency = currencyService.GetCurrencyByTextCode(response.TextCode)
                });
            return rates;
        }

        private ParsingRequest GetRequest(ParsingSettingsDto parsingSettings)
        {
            var request = parsingSettings.Adapt<ParsingRequest>();
            Dictionary<string, string[]> textCommands = GetCommands(parsingSettings.Commands, "TextCode");
            Dictionary<string, string[]> unitCommands = GetCommands(parsingSettings.Commands, "Unit");
            request.TextCodeCommands = textCommands;
            request.UnitCommands = unitCommands;
            return request;
        }

        private Dictionary<string, string[]> GetCommands(IEnumerable<CommandAssignmentDto> commands, string commandsType)
        {
            var keyValuePairs = commands
                    .Where(assignment => assignment.AssignmentFieldName.Name == commandsType)
                    .Select(assignment => new KeyValuePair<string, string[]>
                        (assignment.Command.Name,
                        assignment.CommandParameterValues.Select(value => value.Value).ToArray()));
            var commandDictionary = new Dictionary<string, string[]>(keyValuePairs);
            return commandDictionary;
        }
    }
}
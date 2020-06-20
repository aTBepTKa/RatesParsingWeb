using Mapster;
using MassTransit;
using ParsingMessages.Parsing;
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

        public ParsingService(IRequestClient<IParsingRequest> client, ICurrencyService currency)
        {
            requestClient = client;
            currencyService = currency;
        }

        public async Task<ParsingResultDto> GetExchangeRates(ParsingSettingsDto parsingSettings, string taskName)
        {
            try
            {
                var request = GetRequest(parsingSettings);
                request.TaskName = taskName;
                var response = (await requestClient.GetResponse<IParsingResponse>(request)).Message;
                var result = response.Adapt<ParsingResultDto>();
                if (result.IsSuccesfullParsed)
                    result.ExchangeRates = response.Message.Select(response =>
                            new ExchangeRateDto
                            {
                                Unit = response.Unit,
                                ExchangeRateValue = response.ExchangeRateValue,
                                Currency = currencyService.GetCurrencyByTextCode(response.TextCode)
                            });

                return result;
            }
            catch (Exception ex)
            {
                var result = new ParsingResultDto()
                {
                    IsSuccesfullParsed = false,
                    ErrorDescription = $"Ошибка при получении данных от сервиса парсинга: {ex.Message}"
                };
                return result;
            }
        }

        private ParsingRequestDto GetRequest(ParsingSettingsDto parsingSettings)
        {
            var request = parsingSettings.Adapt<ParsingRequestDto>();
            Dictionary<string, string[]> textCommands = GetCommands(parsingSettings.Commands, "TextCode");
            Dictionary<string, string[]> unitCommands = GetCommands(parsingSettings.Commands, "Unit");
            request.TextCodeCommands = textCommands;
            request.UnitCommands = unitCommands;
            return request;
        }

        private static Dictionary<string, string[]> GetCommands(IEnumerable<CommandAssignmentDto> commands, string commandsType) =>
            commands?.Where(assignment => assignment.AssignmentFieldName.Name == commandsType)
                     .ToDictionary(x => x.Command.Name, x => x.CommandParameterValues.Select(value => value.Value).ToArray());
    }
}

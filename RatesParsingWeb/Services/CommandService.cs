using Mapster;
using MassTransit;
using ParsingMessages.Command;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.Bank.Command;
using RatesParsingWeb.Dto.CommandService;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class CommandService : BaseCrudService<CommandDto, Command>, ICommandService
    {
        private readonly ICommandRepository commandRepository;
        private readonly ICommandParameterRepository commandParameterRepository;
        private readonly IRequestClient<ICommandRequest> requestClient;

        public CommandService(ICommandRepository commandRepository,
                              IRequestClient<ICommandRequest> requestClient,
                              ICommandParameterRepository commandParameterRepository) : base(commandRepository)
        {
            this.commandParameterRepository = commandParameterRepository;
            this.commandRepository = commandRepository;
            this.requestClient = requestClient;
        }

        public async Task<CommandDto> GetCommandWithParameterAsync(int id)
        {
            var command = await commandRepository.GetFirstOrDefaultAsync(x => x.Id == id, i => i.CommandParameters, i => i.CommandFieldName);
            if (command is null)
                throw new ArgumentNullException(nameof(id), $"Команда с Id '{id}' не найдена");
            var commandDto = command.Adapt<CommandDto>();
            return commandDto;
        }


        public async Task<bool> CreateAsync(CommandDto commandCreateDto)
        {
            var command = commandCreateDto.Adapt<Command>();
            await CheckCreateForUniquinessAsync(command);
            CheckForValidity(command);
            if (!ValidationService.IsValid)
                return false;

            await commandRepository.AddAsync(command);
            await commandRepository.SaveChangesAsync();
            return true;
        }

        public async Task<CommandResponseDto> GetExternalCommands(string taskName)
        {
            CommandResponseDto commandResult;
            try
            {
                var request = new CommandRequest(taskName);
                var response = await requestClient.GetResponse<ICommandResponse>(request);
                commandResult = response.Message.Adapt<CommandResponseDto>();
            }
            catch (Exception ex)
            {
                commandResult = new CommandResponseDto()
                {
                    IsSuccesfullParsed = false,
                    ErrorDescription = $"Ошибка при получении списка команд: {ex.Message}"
                };
            }
            return commandResult;
        }

        private void CheckForValidity(Command command)
        {
            // Проверить Command.
            if (string.IsNullOrEmpty(command.Name))
                ValidationService.AddError(nameof(command.Name), "Наименование команды обязательно.");
            else if (command.Name.Length > 20)
                ValidationService.AddError(nameof(command.Name), "Максимальная длина имени команды составляет 20 символов.");

            if (!string.IsNullOrEmpty(command.Description) && command.Description.Length > 200)
                ValidationService.AddError(nameof(command.Description), "Максимальная длина описания команды составляет 200 символов.");
        }

        private async Task CheckCreateForUniquinessAsync(Command command)
        {
            if (await commandRepository.AnyAsync(i => i.Name == command.Name))
                ValidationService.AddError(nameof(command.Name), $"Команда с именем '{command.Name}' уже существует");
        }

        public async Task DeleteAsync(int id)
        {
            var command = await commandRepository.FindAsync(id);
            if (command == null)
                throw new Exception($"Команда с Id '{command.Id}' не найдена.");
            commandRepository.Remove(command);
            await commandRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateParameterAsync(IEnumerable<CommandParameterUpdateDto> commandParameterUpdateDtos)
        {
            foreach (var parameterDto in commandParameterUpdateDtos)
            {
                var parameterToUpdate = await commandParameterRepository
                    .GetFirstOrDefaultAsync(i => i.Id == parameterDto.Id);
                if (parameterToUpdate is null)
                    throw new ArgumentNullException(nameof(parameterDto.Id), $"Команда с Id '{parameterDto.Id}' не найдена.");
                parameterToUpdate.Value = parameterDto.Value;
                commandParameterRepository.SetStateModifed(parameterToUpdate);
            }
            await commandParameterRepository.SaveChangesAsync();
            return true;
        }
    }
}

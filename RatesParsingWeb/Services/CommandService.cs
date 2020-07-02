using Mapster;
using MassTransit;
using ParsingMessages.Command;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.CommandService;
using RatesParsingWeb.Dto.ParsingSettings;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class CommandService : BaseCrudService<CommandDto, Command>, ICommandService
    {
        private readonly ICommandRepository commandRepository;
        private readonly IRequestClient<ICommandRequest> requestClient;

        public CommandService(ICommandRepository commandRepository, IRequestClient<ICommandRequest> requestClient) : base(commandRepository)
        {
            this.commandRepository = commandRepository;
            this.requestClient = requestClient;
        }

        public CommandDto GetCommandWithParameter(int id)
        {
            var command = commandRepository.GetFirstOrDefault(x => x.Id == id, i => i.CommandParameters, i => i.CommandFieldName);
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

        public async Task<bool> UpdateAsync(int id)
        {
            var command = await commandRepository.FindAsync(id);
            if (command == null)
                throw new Exception($"Команда с Id '{command.Id}' не найдена.");
            // TODO: Реализовать валидацию CommandParameter.
            await CheckUpdateForUniquinessAsync(command);
            CheckForValidity(command);
            if (!ValidationService.IsValid)
                return false;
            commandRepository.SetStateModifed(command);
            await commandRepository.SaveChangesAsync();
            return true;
        }

        private async Task CheckUpdateForUniquinessAsync(Command command)
        {
            if (await commandRepository.AnyAsync(i => i.Id != command.Id && i.Name == command.Name))
                ValidationService.AddError(nameof(command.Name), "Команда с таким именем уже существует");
        }
    }
}

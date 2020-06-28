using Mapster;
using MassTransit;
using ParsingMessages.Command;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.CommandService;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class CommandService : BaseCrudService<CommandDto, Command>, ICommandService
    {
        private readonly ICommandRepository commandRepository;
        private readonly IRequestClient<ICommandRequest> requestClient;

        public CommandService(ICommandRepository command, IRequestClient<ICommandRequest> request) : base(command)
        {
            commandRepository = command;
            requestClient = request;
        }

        public async Task<IEnumerable<CommandDto>> GetCommandListWithParameterAsync()
        {
            var commands = await commandRepository.GetAllAsync(i => i.CommandParameters);
            // Mapster не позволяет сконвертировать IEnumerable<Command> в IEnumerable<CommandDto> 
            // видимо из-за свойства IEnumerable<ICommandParameter> CommandParameters в классе CommandDto.
            var commandsDto = commands.Select(x => new CommandDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CommandParameters = x.CommandParameters.Select(y=>new CommandParameterDto()
                {
                    Description = y.Description,
                    Name = y.Name
                })
            });
            return commandsDto;
        }
        public async Task<CommandDto> GetCommandWithParameterAsync(int id) =>
            (await commandRepository
            .GetFirstOrDefaultAsync(i => i.Id == id, i => i.CommandParameters))
            .Adapt<CommandDto>();


        public async Task<bool> CreateAsync(CommandCreateDto commandCreateDto)
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

        public async Task<ICommandResponse> GetExternalCommands(string taskName)
        {
            ICommandResponse commands;
            try
            {
                var request = new CommandRequest(taskName);
                var response = await requestClient.GetResponse<ICommandResponse>(request);
                commands = response.Message;
            }
            catch (Exception ex)
            {
                commands = new CommandResultDto()
                {
                    IsSuccesfullParsed = false,
                    ErrorDescription = $"Ошибка при получении списка команд: {ex.Message}"
                };
            }
            return commands;
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

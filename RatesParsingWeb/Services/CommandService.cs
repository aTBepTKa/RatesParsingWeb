using Mapster;
using MassTransit;
using ParsingMessages.Command;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.CommandService;
using RatesParsingWeb.Dto.ParsingSettings;
using RatesParsingWeb.Dto.UpdateAndCreate;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class CommandService : BaseCrudService<CommandAssignmentDto, CommandAssignment>, ICommandService
    {
        private readonly ICommandAssignmentRepository commandAssignmentRepository;
        private readonly IRequestClient<ICommandRequest> requestClient;

        public CommandService(ICommandAssignmentRepository commandAssignmentRepository, IRequestClient<ICommandRequest> requestClient) : base(commandAssignmentRepository)
        {
            this.commandAssignmentRepository = commandAssignmentRepository;
            this.requestClient = requestClient;
        }

        public CommandAssignmentDto GetCommandWithParameter(int id)
        {
            var command = commandAssignmentRepository.GetWithParameters(id);
            var commandDto = command.Adapt<CommandAssignmentDto>();
            return commandDto;
        }


        public async Task<bool> CreateAsync(CommandAssignmentDto commandCreateDto)
        {
            var command = commandCreateDto.Adapt<CommandAssignment>();
            await CheckCreateForUniquinessAsync(command);
            CheckForValidity(command);
            if (!ValidationService.IsValid)
                return false;

            await commandAssignmentRepository.AddAsync(command);
            await commandAssignmentRepository.SaveChangesAsync();
            return true;
        }

        public async Task<CommandResultDto> GetExternalCommands(string taskName)
        {
            CommandResultDto commandResult;
            try
            {
                var request = new CommandRequest(taskName);
                var response = await requestClient.GetResponse<ICommandResponse>(request);
                commandResult = response.Message.Adapt<CommandResultDto>();
            }
            catch (Exception ex)
            {
                commandResult = new CommandResultDto()
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

        private async Task CheckCreateForUniquinessAsync(CommandAssignment command)
        {
            if (await commandAssignmentRepository.AnyAsync(i => i.Command.Name == command.Command.Name))
                ValidationService.AddError(nameof(command.Name), $"Команда с именем '{command.Name}' уже существует");
        }

        public async Task DeleteAsync(int id)
        {
            var command = await commandAssignmentRepository.FindAsync(id);
            if (command == null)
                throw new Exception($"Команда с Id '{command.Id}' не найдена.");
            commandAssignmentRepository.Remove(command);
            await commandAssignmentRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id)
        {
            var command = await commandAssignmentRepository.FindAsync(id);
            if (command == null)
                throw new Exception($"Команда с Id '{command.Id}' не найдена.");
            // TODO: Реализовать валидацию CommandParameter.
            await CheckUpdateForUniquinessAsync(command);
            CheckForValidity(command);
            if (!ValidationService.IsValid)
                return false;
            commandAssignmentRepository.SetStateModifed(command);
            await commandAssignmentRepository.SaveChangesAsync();
            return true;
        }

        private async Task CheckUpdateForUniquinessAsync(Command command)
        {
            if (await commandAssignmentRepository.AnyAsync(i => i.Id != command.Id && i.Name == command.Name))
                ValidationService.AddError(nameof(command.Name), "Команда с таким именем уже существует");
        }
    }
}

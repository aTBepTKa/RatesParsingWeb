using Mapster;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.ExternalCommand;
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
        private readonly ICommandRepository commandRepository
            ;

        public CommandService(ICommandRepository command) : base(command)
        {
            commandRepository = command;
        }

        public async Task<IEnumerable<CommandDto>> GetCommandParameterListAsync() =>
            (await commandRepository.GetAllAsync(i => i.CommandParameters)).Adapt<IEnumerable<CommandDto>>();

        public async Task<CommandDto> GetCommandParameterAsync(int id) =>
            (await commandRepository
            .GetFirstOrDefaultAsync(i => i.Id == id, i => i.CommandParameters))
            .Adapt<CommandDto>();


        public async Task<bool> CreateAsync(CommandCreateDto commandCreateDto)
        {
            var command = commandCreateDto.Adapt<Command>();
            await CheckCreateForUniquinessAsync(command);
            CheckForValidity(command);
            if (!ValidationDictionary.IsValid)
                return false;

            await commandRepository.AddAsync(command);
            await commandRepository.SaveChangesAsync();
            return true;
        }

        public IEnumerable<CommandCreateDto> GetExternalCommands()
        {
            var commandsFactory = new FakeTaxi();
            IEnumerable<ExternalCommandDto> externalCommands = commandsFactory.GetCommands();
            return externalCommands.Adapt<IEnumerable<CommandCreateDto>>();
        }

        private void CheckForValidity(Command command)
        {
            // Проверить Command.
            if (string.IsNullOrEmpty(command.Name))
                ValidationDictionary.AddError(nameof(command.Name), "Наименование команды обязательно.");
            else if (command.Name.Length > 20)
                ValidationDictionary.AddError(nameof(command.Name), "Максимальная длина имени команды составляет 20 символов.");

            if (!string.IsNullOrEmpty(command.Description) && command.Description.Length > 200)
                ValidationDictionary.AddError(nameof(command.Description), "Максимальная длина описания команды составляет 200 символов.");
        }

        private async Task CheckCreateForUniquinessAsync(Command command)
        {
            if (await commandRepository.AnyAsync(i => i.Name == command.Name))
                ValidationDictionary.AddError(nameof(command.Name), $"Команда с именем '{command.Name}' уже существует");
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
            if (!ValidationDictionary.IsValid)
                return false;
            commandRepository.SetStateModifed(command);
            await commandRepository.SaveChangesAsync();
            return true;
        }



        private async Task CheckUpdateForUniquinessAsync(Command command)
        {
            if (await commandRepository.AnyAsync(i => i.Id != command.Id && i.Name == command.Name))
                ValidationDictionary.AddError(nameof(command.Name), "Команда с таким именем уже существует");
        }
    }
}

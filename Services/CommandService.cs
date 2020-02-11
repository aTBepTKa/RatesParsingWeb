using Mapster;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
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

        public async Task<IEnumerable<CommandDto>> GetCommandsWithParameters() =>
            (await commandRepository.GetAllAsync(i => i.CommandParameters)).Adapt<IEnumerable<CommandDto>>();

        public async Task<bool> CreateAsync(CommandCreateDto commandCreateDto)
        {
            await CheckCreateForUniquinessAsync(commandCreateDto);
            CheckForValidity(commandCreateDto);
            if (!ValidationDictionary.IsValid)
                return false;

            var command = commandCreateDto.Adapt<Command>();
            await commandRepository.AddAsync(command);
            await commandRepository.SaveChangesAsync();
            return true;
        }

        private void CheckForValidity(ICommandValidity command)
        {
            // Проверить Command.
            if (string.IsNullOrEmpty(command.Name))
                ValidationDictionary.AddError(nameof(command.Name), "Краткое наименование команды обязательно.");
            else if (command.Name.Length > 20)
                ValidationDictionary.AddError(nameof(command.Name), "Максимальная длина краткого имени команды составляет 20 символов.");
            
            if (!string.IsNullOrEmpty(command.FullName) && command.FullName.Length > 50)
                ValidationDictionary.AddError(nameof(command.FullName), "Максимальная длина полного имени команды составляет 50 символов.");            
        }

        private async Task CheckCreateForUniquinessAsync(CommandCreateDto command)
        {
            if (await commandRepository.AnyAsync(i => i.Name == command.Name))
                ValidationDictionary.AddError(nameof(command.Name), "Команда с таким именем уже существует");
        }

        private async Task CheckUpdateForUniquinessAsync(CommandUpdateDto command)
        {
            if (await commandRepository.AnyAsync(i => i.Id != command.Id && i.Name == command.Name))
                ValidationDictionary.AddError(nameof(command.Name), "Команда с таким именем уже существует");
        }
    }
}

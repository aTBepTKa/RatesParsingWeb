using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto;
using RatesParsingWeb.Dto.UpdateAndCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface ICommandService : ICrudService<CommandDto>
    {
        /// <summary>
        /// Создать команду.
        /// </summary>
        /// <param name="commandCreateDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(CommandCreateDto commandCreateDto);

        /// <summary>
        /// Возвращает объект команды с зависимой таблицей параметров.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CommandDto>> GetCommandsWithParameters();
    }
}

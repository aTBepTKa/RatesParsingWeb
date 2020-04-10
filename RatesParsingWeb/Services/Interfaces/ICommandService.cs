using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.CommandService;
using RatesParsingWeb.Dto.UpdateAndCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    /// <summary>
    /// Представляет средства для работы с командами для парсинга страниц.
    /// </summary>
    public interface ICommandService : ICrudService<CommandDto>
    {
        /// <summary>
        /// Создать команду.
        /// </summary>
        /// <param name="commandCreateDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(CommandCreateDto commandCreateDto);

        /// <summary>
        /// Удалить команду по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Обновить данные банка.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(int id);

        /// <summary>
        /// Получить список команд с таблицей параметров.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CommandDto>> GetCommandParameterListAsync();

        /// <summary>
        /// Получить команду с таблицей параметров
        /// </summary>
        /// <returns></returns>
        Task<CommandDto> GetCommandParameterAsync(int id);

        /// <summary>
        /// Получить список команд из внешнего источника.
        /// </summary>
        /// <returns></returns>
        Task<CommandResultDto> GetExternalCommands(string taskName = "Получить команды");
    }
}

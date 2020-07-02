using RatesParsingWeb.Dto.CommandService;
using RatesParsingWeb.Dto.ParsingSettings;
using RatesParsingWeb.Dto.UpdateAndCreate;
using System.Collections.Generic;
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
        Task<bool> CreateAsync(CommandDto commandCreateDto);

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
        /// Получить команду с таблицей параметров.
        /// </summary>
        /// <returns></returns>
        CommandDto GetCommandWithParameter(int id);

        /// <summary>
        /// Получить список команд из внешнего источника.
        /// </summary>
        /// <returns></returns>
        Task<CommandResponseDto> GetExternalCommands(string taskName = "Получить команды");
    }
}

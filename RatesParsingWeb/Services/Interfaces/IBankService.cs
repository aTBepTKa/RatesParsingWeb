using RatesParsingWeb.Dto.Bank;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IBankService : ICrudService<BankDto>
    {
        /// <summary>
        /// Получить все банки с основной валютой.
        /// </summary>
        /// <returns></returns>      
        Task<IEnumerable<BankDto>> GetListAsync();

        /// <summary>
        /// Получить банк с основной валютой.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BankDto> GetWithCurrencyAsync(int id);

        /// <summary>
        /// Получить банк с настройками парсинга.
        /// </summary>
        /// <param name="id">Id банка</param>
        /// <returns></returns>
        Task<BankDto> GetWithParsingSettingsAsync(int id);

        /// <summary>
        /// Получить банк с настройками парсинга.
        /// </summary>
        /// <param name="swiftCode">SWIFT-код банка.</param>
        /// <returns></returns>
        Task<BankDto> GetWithParsingSettingsAsync(string swiftCode);

        /// <summary>
        /// Создать новый банк.
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(BankCreateDto createDto);

        /// <summary>
        /// Обновить данные банка.
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BankUpdateDto updateDto);

        /// <summary>
        /// Удалить банк по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}

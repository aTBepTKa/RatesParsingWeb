using RatesParsingWeb.Domain;
using RatesParsingWeb.Dto.ParsingSettings;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IParsingSettingsService : ICrudService<ParsingSettingsDto>
    {
        Task<ParsingSettingsDto> GetSettings(int id);
    }
}
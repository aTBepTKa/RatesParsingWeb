using Mapster;
using RatesParsingWeb.Services.Interfaces;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    /// <summary>
    /// Сервис для работы с данными.
    /// </summary>
    /// <typeparam name="DtoType"></typeparam>
    public abstract class BaseCrudService<DtoType, RepositoryType> : ICrudService<DtoType> where DtoType : class where RepositoryType : class
    {
        protected BaseCrudService(IRepository<RepositoryType> baseRepository)
        {
            BaseRepository = baseRepository;
            ValidationDictionary = new ValidationDictionary();
        }

        public IValidationService ValidationDictionary { get; }

        protected IRepository<RepositoryType> BaseRepository { get; }

        public async Task<IEnumerable<DtoType>> GetAllAsync() =>
            (await BaseRepository.GetAllAsync()).Adapt<IEnumerable<DtoType>>();

        public async Task<DtoType> GetByIdAsync(int id) =>
            (await BaseRepository.FindAsync(id)).Adapt<DtoType>();
    }
}

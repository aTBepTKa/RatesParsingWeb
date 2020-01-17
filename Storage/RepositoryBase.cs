using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;

namespace RatesParsingWeb.Storage
{
    /// <summary>
    /// Базовый класс для работы с сущностями БД.
    /// </summary>
    public class RepositoryBase<T> : IRepositoryBase<T>
    {
        protected readonly BankRatesContext context;

        public RepositoryBase(DbContextOptions<BankRatesContext> options)
        {
            context = new BankRatesContext(options);
            
        }

        public void AddAndSaveAsync(T t)
        {
            context.Add(t);
            context.SaveChangesAsync();
        }
    }
}

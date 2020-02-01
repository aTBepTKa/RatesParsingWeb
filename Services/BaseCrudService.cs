using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RatesParsingWeb.Storage;
using RatesParsingWeb.Storage.Repositories;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mapster;

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
        }

        protected IRepository<RepositoryType> BaseRepository { get; set; }

        public virtual async Task<IEnumerable<DtoType>> GetAllAsync() =>
            (await BaseRepository.GetAllAsync()).Adapt<IEnumerable<DtoType>>();        
    }
}

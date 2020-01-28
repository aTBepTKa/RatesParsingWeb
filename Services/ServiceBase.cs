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

namespace RatesParsingWeb.Services
{    
    /// <summary>
    /// Сервис для работы с данными.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ServiceBase<T> : IService<T> where T : class
    {
        protected RepositoryBase<T> RepositoryBase { get; set; }

        public virtual async Task AddAsync(T entity)=>
                await RepositoryBase.AddAsync(entity);

        public virtual Task AddRangeAsync(T[] entity) =>
            RepositoryBase.AddRangeAsync(entity);


        public virtual async Task<T> GetByIdAsync(int id) =>
            await RepositoryBase.GetByIdAsync(id);

        public virtual Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where) =>
            RepositoryBase.GetFirstOrDefaultAsync(where);

        public virtual async Task<IEnumerable<T>> GetAll() =>
            await RepositoryBase.GetAll();

        public virtual async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where) =>
            await RepositoryBase.GetMany(where);

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> where) =>
            RepositoryBase.AnyAsync(where);

        public virtual Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
                return RepositoryBase.CountAsync();
            else
                return RepositoryBase.CountAsync(where);
        }

        public IQueryable<T> Query() =>
            RepositoryBase.Query();

        public virtual async Task Commit() =>
            await RepositoryBase.SaveChangesAsync();

        public virtual void SetStateModifed(T t) =>
            RepositoryBase.SetStateModifed(t);

        public abstract bool IsValid(T t, ModelStateDictionary modelState);
    }
}

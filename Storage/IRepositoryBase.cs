using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage
{
    public interface IRepositoryBase<T>
    {
        void AddAndSaveAsync(T t);
    }
}

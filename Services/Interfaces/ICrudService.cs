using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        /// <summary>
        /// Является ли объект валидным.
        /// </summary>
        /// <returns></returns>
        bool IsValid(T t);
    }
}

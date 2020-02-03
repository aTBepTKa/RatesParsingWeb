using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services.Interfaces
{
    public interface IValidationDictionary
    {
        IDictionary<string, string> ErrorDictioanry { get; }

        void AddError(string key, string value);

        bool IsValid { get; }
    }
}

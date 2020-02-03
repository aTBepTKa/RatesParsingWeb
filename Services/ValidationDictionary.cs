using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{
    public class ValidationDictionary : IValidationDictionary
    {
        public ValidationDictionary()
        {
            ErrorDictioanry = new Dictionary<string, string>();
        }

        public bool IsValid => ErrorDictioanry.Count == 0;

        public IDictionary<string, string> ErrorDictioanry { get; }

        public void AddError(string key, string value)
        {
            ErrorDictioanry.Add(key, value);
        }
    }
}

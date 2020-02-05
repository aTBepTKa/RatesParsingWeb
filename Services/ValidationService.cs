using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{

    public class ValidationDictionary : IValidationService
    {
        public ValidationDictionary()
        {
            ErrorDictioanry = new Dictionary<string, List<string>>();
        }

        public bool IsValid => ErrorDictioanry.Count == 0;

        public IDictionary<string, List<string>> ErrorDictioanry { get; }

        public void AddError(string key, string value)
        {
            if (!ErrorDictioanry.ContainsKey(key))
                ErrorDictioanry.Add(key, new List<string>());
            ErrorDictioanry[key].Add(value);
        }

        public IEnumerable<string> ErrorListWithKeys => GetErrorList(true);

        public IEnumerable<string> ErrorListWithoutKeys => GetErrorList(false);

        private IEnumerable<string> GetErrorList(bool withKeys)
        {
            if (withKeys)
                return ErrorDictioanry.SelectMany(keyValuePair => keyValuePair.Value.Select(error => $"{keyValuePair.Key}ХУЙ: {error}"));
            else
                return ErrorDictioanry.SelectMany(keyValuePair => keyValuePair.Value);
        }
    }
}

using RatesParsingWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Services
{

    public class ValidationService : IValidationService
    {
        public ValidationService()
        {
            ErrorDictioanry = new Dictionary<string, List<string>>();
        }

        public bool IsValid => ErrorDictioanry.Count == 0;

        public IDictionary<string, List<string>> ErrorDictioanry { get; }

        public void AddError(string key, string value)
        {
            if (ErrorDictioanry.TryGetValue(key, out List<string> errors))
                errors.Add(value);
            else
                ErrorDictioanry.Add(key, new List<string> { value });
        }

        public IEnumerable<string> ErrorListWithKeys => GetErrorList(true);

        public IEnumerable<string> ErrorListWithoutKeys => GetErrorList(false);

        private IEnumerable<string> GetErrorList(bool withKeys)
        {
            if (withKeys)
                return ErrorDictioanry.SelectMany(keyValuePair => keyValuePair.Value.Select(error => $"{keyValuePair.Key}: {error}"));
            else
                return ErrorDictioanry.SelectMany(keyValuePair => keyValuePair.Value);
        }
    }
}

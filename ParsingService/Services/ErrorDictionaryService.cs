using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsingService.Services
{

    public class ErrorDictionaryService : IErrorDictionaryService
    {
        private Dictionary<string, List<string>> errorDictioanry { get; }
        public ErrorDictionaryService()
        {
            errorDictioanry = new Dictionary<string, List<string>>();
        }

        public bool IsValid => errorDictioanry.Count == 0;

        public void AddError(string key, string value)
        {
            if (!errorDictioanry.ContainsKey(key))
                errorDictioanry.Add(key, new List<string>());
            errorDictioanry[key].Add(value);
        }

        public Dictionary<string, IEnumerable<string>> ErrorDictionary =>
            errorDictioanry.Adapt<Dictionary<string, IEnumerable<string>>>();

        public IEnumerable<string> ErrorListWithKeys => GetErrorList(true);

        public IEnumerable<string> ErrorListWithoutKeys => GetErrorList(false);

        private IEnumerable<string> GetErrorList(bool withKeys)
        {
            if (withKeys)
                return errorDictioanry.SelectMany(keyValuePair => keyValuePair.Value.Select(error => $"{keyValuePair.Key}: {error}"));
            else
                return errorDictioanry.SelectMany(keyValuePair => keyValuePair.Value);
        }
    }
}

using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsingService.Services
{

    public class ErrorDictionaryService : IErrorDictionaryService
    {
        private Dictionary<string, List<string>> ErrorDictioanry { get; }
        public ErrorDictionaryService()
        {
            ErrorDictioanry = new Dictionary<string, List<string>>();
        }

        public bool IsValid => ErrorDictioanry.Count == 0;

        public void AddError(string key, string value)
        {
            if (!ErrorDictioanry.ContainsKey(key))
                ErrorDictioanry.Add(key, new List<string>());
            ErrorDictioanry[key].Add(value);
        }

        public Dictionary<string, IEnumerable<string>> ErrorDictionary =>
            ErrorDictioanry.Adapt<Dictionary<string, IEnumerable<string>>>();

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

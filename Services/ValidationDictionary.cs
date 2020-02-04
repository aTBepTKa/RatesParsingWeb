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

        public IEnumerable<string> GetErrorListWithKeys() =>
            GetErrorList(true);

        public IEnumerable<string> GetErrorListWithoutKeys() =>
            GetErrorList(false);

        private IEnumerable<string> GetErrorList(bool withKeys)
        {
            if (ErrorDictioanry.Count == 0)
                return Enumerable.Empty<string>();

            List<string> errorList = new List<string>();
            foreach (var property in ErrorDictioanry)
            {
                foreach (var error in property.Value)
                {
                    string errorMessage;
                    if (withKeys)
                        errorMessage = $"{property.Key}: {error}";
                    else
                        errorMessage = error;
                    errorList.Add(errorMessage);
                }
            }
            return errorList;
        }
    }
}

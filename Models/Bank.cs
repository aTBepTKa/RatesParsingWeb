using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class Bank
    {
        public int ID { get; set; }
        public string BankName { get; set; }
        public string BankUrl { get; set; }
        public string RatesUrl { get; set; }
        public int CurrencyID { get; set; }

        #region Xpathes
        public string TextCodeXpath { get; set; }
        public string UnitXpath { get; set; }
        public string ExchangeRateXpath { get; set; }
        public string VariablePartOfXpath { get; set; }
        public int StartXpathRow { get; set; }
        public int EndXpathRow { get; set; }
        #endregion

        public string NumberDecimalSeparator { get; set; }
        public string NumberGroupSeparator { get; set; }

        public Currency Currency { get; set; }
        public ICollection<ScriptAssignment> UnitScripts { get; set; }
        public ICollection<ScriptAssignment> TextCodeScripts { get; set; }
        public ICollection<ExchangeRateList> ExchangeRateLists { get; set; }
    }
}

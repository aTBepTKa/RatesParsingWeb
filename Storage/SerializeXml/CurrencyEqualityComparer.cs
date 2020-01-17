using System.Collections.Generic;

namespace RatesParsingWeb.Storage.SerializeXml
{
    /// <summary>
    /// Представляет средства для сравнивания объектов CurrencyXmlItem по текстовому коду валюты.
    /// </summary>
    public class CurrencyEqualityComparer : IEqualityComparer<CurrencyXmlItem>
    {
        public bool Equals(CurrencyXmlItem x, CurrencyXmlItem y)
        {
            if (x.Ccy == null && y.Ccy == null)
                return true;
            if ((x.Ccy != null && y.Ccy == null) || (x.Ccy == null && y.Ccy != null))
                return false;
            return x.Ccy == y.Ccy;
        }

        public int GetHashCode(CurrencyXmlItem obj)
        {
            if (obj.Ccy == null)
                return 0;
            return obj.Ccy.GetHashCode();
        }
    }
}

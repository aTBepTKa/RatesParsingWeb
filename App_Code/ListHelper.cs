using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RatesParsingWeb.App_Code
{
    /// <summary>
    /// Html хэлперы для работы с листом.
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// Создать немаркированный лист ошибок.
        /// </summary>
        /// <param name="_"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static HtmlString CreteErrorList(this IHtmlHelper _, IEnumerable<string> items)
        {
            var listAttribute = new KeyValuePair<string, string>("class", "list - group - item - danger");
            TagBuilder ul = new TagBuilder("ul");
            foreach (var item in items)
            {
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml.Append(item);
                li.Attributes.Add(listAttribute);
                ul.InnerHtml.AppendHtml(li);
            }
            ul.Attributes.Add(listAttribute);
            var writer = new System.IO.StringWriter();
            ul.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}

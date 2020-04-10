using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Models.ParsingSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.IO;

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
            var listAttribute = new KeyValuePair<string, string>("class", "list-group-item-danger");
            TagBuilder ul = new TagBuilder("ul");
            foreach (var item in items)
            {
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml.Append(item);
                ul.InnerHtml.AppendHtml(li);
            }
            ul.Attributes.Add(listAttribute);
            var writer = new StringWriter();
            ul.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        /// <summary>
        /// Создать немаркированный лист команд.
        /// </summary>
        /// <param name="_"></param>
        /// <param name="items"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static HtmlString CreateCommandList(this IHtmlHelper _, IEnumerable<CommandAssignmentModel> items, string fieldName)
        {
            IHtmlContent htmlContent;
            if (!items.Any())
            {
                var div = new TagBuilder("div");
                div.InnerHtml.Append("Кооманды отсутствуют.");
                htmlContent = div;
            }
            else
            {
                var ul = new TagBuilder("ul");
                foreach(var item in items)
                {
                    var li = new TagBuilder("li");
                    li.InnerHtml.Append(item.Command.Name);
                    ul.InnerHtml.AppendHtml(li);
                }
                htmlContent = ul;
            }
            var htmlString = GetListHtmlString(htmlContent);
            return htmlString;
        }

        private static HtmlString GetListHtmlString(IHtmlContent list)
        {
            var writer = new StringWriter();
            list.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}

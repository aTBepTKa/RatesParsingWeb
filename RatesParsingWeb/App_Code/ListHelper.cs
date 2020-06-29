using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatesParsingWeb.Models.ParsingSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        /// Создать немаркированный список команд.
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
                div.InnerHtml.Append("Команды отсутствуют.");
                htmlContent = div;
            }
            else
            {
                var ul = new TagBuilder("ul");
                foreach (var item in items)
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

        /// <summary>
        /// Создать редактируемую таблицу команд.
        /// </summary>
        /// <param name="_"></param>
        /// <param name="commands"></param>
        /// <returns></returns>
        public static HtmlString CreateEditableCommandList(this IHtmlHelper _, IEnumerable<CommandAssignmentModel> commands)
        {


            if (!commands.Any())
            {
                var div = new TagBuilder("div");
                div.InnerHtml.Append("Команды отсутствуют.");
                return GetListHtmlString(div);
            }
            var table = new TagBuilder("table");
            table.AddCssClass("table");

            // Заголовок таблицы.
            var thead = new TagBuilder("thead");
            var trThead = new TagBuilder("tr");
            trThead.InnerHtml.AppendHtml("<th>Команда</th>");
            trThead.InnerHtml.AppendHtml("<th>Параметры</th>");
            trThead.InnerHtml.AppendHtml("<th>Действие</th>");
            thead.InnerHtml.AppendHtml(trThead);
            table.InnerHtml.AppendHtml(thead);

            // Тело таблицы.
            var tbody = new TagBuilder("tbody");
            foreach (var command in commands)
            {
                var tr = new TagBuilder("tr");
                var tdName = new TagBuilder("td");
                var tdParameter = new TagBuilder("td");
                var tdAction = new TagBuilder("td");

                // Наименование команды.
                var divCommandName = new TagBuilder("h6");
                divCommandName.InnerHtml.Append(command.Command.Name);
                var divCommandDescription = new TagBuilder("div");
                divCommandDescription.InnerHtml.Append(command.Command.Description);
                tdName.InnerHtml.AppendHtml(divCommandName);
                tdName.InnerHtml.AppendHtml(divCommandDescription);

                // Парметры команды.
                foreach (var parameter in command.Command.CommandParameters)
                {
                    var label = new TagBuilder("label");
                    label.MergeAttribute("asp-for", nameof(parameter.Name));
                    label.AddCssClass("control-label");
                    tdParameter.InnerHtml.AppendHtml(label);
                }


                // Формирование разметки.
                tr.InnerHtml.AppendHtml(tdName);
                tr.InnerHtml.AppendHtml(tdParameter);
                tr.InnerHtml.AppendHtml(tdAction);
                tbody.InnerHtml.AppendHtml(tr);
            }
            table.InnerHtml.AppendHtml(tbody);

            return GetListHtmlString(table);
        }

        private static HtmlString GetListHtmlString(IHtmlContent list)
        {
            var writer = new StringWriter();
            list.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}

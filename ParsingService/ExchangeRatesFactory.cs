using HtmlAgilityPack;
using ParsingService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace ParsingService
{
    /// <summary>
    /// Представляет средства для парсинга страницы банка.
    /// </summary>
    class ExchangeRatesFactory
    {
        /// <summary>
        /// Получить курсы валют банка асинхронно.
        /// </summary>
        /// <param name="request">Данные для запроса к банку.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ExchangeRate>> GetBankRatesAsync(BankRequest request)
        {
            var html = new HtmlWeb();
            HtmlDocument htmlDocument = await html.LoadFromWebAsync(request.RatesUrl);

            if (html == null)
                return Array.Empty<ExchangeRate>();

            // Получить методы для обработки строк.
            WordProcessingHandler textCodeProcessor = GetMethods(request.TextCodeCommands);
            WordProcessingHandler unitProcessor = GetMethods(request.UnitCommands);

            var currencyList = new List<ExchangeRate>(request.EndXpathRow - request.StartXpathRow + 1);
            for (var i = request.StartXpathRow; i <= request.EndXpathRow; i++)
            {
                var currencyData = new ExchangeRate();

                var textCodeXpath = GetActualXpath(request.TextCodeXpath, request.VariablePartOfXpath, i.ToString());
                currencyData.TextCode = GetTextCode(textCodeXpath, htmlDocument, textCodeProcessor);

                var unitXpath = GetActualXpath(request.UnitXpath, request.VariablePartOfXpath, i.ToString());
                currencyData.Unit = GetUnit(unitXpath, htmlDocument, unitProcessor);

                var rateXpath = GetActualXpath(request.ExchangeRateXpath, request.VariablePartOfXpath, i.ToString());
                currencyData.ExchangeRateValue = GetRate(rateXpath, htmlDocument,
                    request.NumberDecimalSeparator, request.NumberGroupSeparator);

                currencyList.Add(currencyData);
            }
            return currencyList;
        }

        /// <summary>
        /// Получить методы из словаря.
        /// </summary>
        /// <param name="methodNames">Наименование методов и соответствующие параметры.</param>
        /// <returns></returns>
        private WordProcessingHandler GetMethods(IDictionary<string, string[]> methodNames)
        {
            if (methodNames != null)
            {
                WordProcessingHandler methods = null;
                // Получить тип объекта, содержащего методы.
                Type scriptsType = typeof(Commands);
                // Создать объект, содержащий методы.
                object scriptsObject = Activator.CreateInstance(scriptsType);
                // Получить список методов.
                foreach (var methodName in methodNames)
                {
                    // Получить метод по заданному имени из словаря.
                    MethodInfo method = scriptsType.GetMethod(methodName.Key);
                    var newMethod = method.Invoke(scriptsObject, methodName.Value);
                    methods += newMethod as WordProcessingHandler;
                }
                return methods;
            }
            else
                return text => text;
        }

        /// <summary>
        /// Получить текстовый код валюты по XPath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="html"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private string GetTextCode(string xpath, HtmlDocument html, WordProcessingHandler handler)
        {
            string textCode = GetValueByXPath(html, xpath);
            textCode = handler(textCode);
            return textCode;
        }

        /// <summary>
        /// Получить единицу измерения валюты по XPath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="html"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private int GetUnit(string xpath, HtmlDocument html, WordProcessingHandler handler)
        {
            string unit = GetValueByXPath(html, xpath);
            unit = handler(unit);

            if (int.TryParse(unit, out int unitResult))
                return unitResult;
            else
                return 0;
        }

        /// <summary>
        /// Получить обеменный курс валюты по XPath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="html"></param>
        /// <param name="decimalSeparator"></param>
        /// <param name="groupSeparator"></param>
        /// <returns></returns>
        private decimal GetRate(string xpath, HtmlDocument html, string decimalSeparator, string groupSeparator)
        {
            var formatInfo = new NumberFormatInfo
            {
                NumberDecimalSeparator = decimalSeparator,
                NumberGroupSeparator = groupSeparator
            };
            string exchangeRate = GetValueByXPath(html, xpath);
            if (decimal.TryParse(exchangeRate, NumberStyles.Currency, formatInfo, out decimal exchangeRateResult))
                return exchangeRateResult;
            else
                return 0;
        }

        /// <summary>
        /// Получить значение по адресу XPath.
        /// </summary>
        /// <param name="html">Страница для парсинга.</param>
        /// <param name="xpath">Адрес XPath искомого значения.</param>
        /// <returns></returns>
        private static string GetValueByXPath(HtmlDocument html, string xpath)
        {
            // Узел целевого значения.
            HtmlNode resultNode;
            // Целевое значение.
            string result;

            // Попытка получить узел.
            try
            {
                resultNode = html.DocumentNode.SelectSingleNode(xpath);
            }
            catch (System.Xml.XPath.XPathException)
            {
                throw new System.Xml.XPath.XPathException($"Ошибка при обработке XPath адреса {xpath}. ");
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Отсутствует XPath адрес");
            }

            if (resultNode != null)
            {
                result = resultNode.InnerText;
                // Привести форматирование текста к приемлемому виду.
                result = GetClearText(result);
            }
            else
                throw new ArgumentNullException("При поиске по адресу XPath получено значение Null");
            return result;
        }

        /// <summary>
        /// Получить текст без лишних пробелов, новых строк и т. п.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string GetClearText(string text)
        {
            text = text.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            return text;
        }

        /// <summary>
        /// Получить Xpath путем подстановки актуального значения вместо переменной часть.
        /// </summary>
        /// <param name="initialXpath">XPath адрес.</param>
        /// <param name="variablePart">Переменная часть XPath адреса</param>
        /// <param name="variableValue">Значение переменной части XPath адреса</param>
        /// <returns></returns>
        private string GetActualXpath(string initialXpath, string variablePart, string variableValue) =>
            initialXpath.Replace(variablePart, variableValue);
    }
}

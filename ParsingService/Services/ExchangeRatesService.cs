using HtmlAgilityPack;
using ParsingService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace ParsingService.Services
{
    /// <summary>
    /// Представляет средства для парсинга страницы банка.
    /// </summary>
    static class ExchangeRatesService
    {
        /// <summary>
        /// Получить курсы валют банка асинхронно.
        /// </summary>
        /// <param name="request">Данные для запроса к банку.</param>
        /// <returns></returns>
        public static async Task<ParsingResult> GetBankRatesAsync(ParsingRequest request)
        {
            var result = new ParsingResult();
            try
            {
                result.Message = await GetRatesAsync(request);
            }
            catch (HttpRequestException ex)
            {
                result.SetError($"Ошибка при выполнении запроса к сайту {request.RatesUrl}: {ex.Message}");
            }
            catch (Exception ex)
            {
                result.SetError($"Ошибка при выполнении парсинга страницы: {ex.Message}");
            }
            return result;
        }

        private static async Task<IEnumerable<ExchangeRate>> GetRatesAsync(ParsingRequest request)
        {
            var html = new HtmlWeb();
            HtmlDocument htmlDocument;
            htmlDocument = await html.LoadFromWebAsync(request.RatesUrl);

            // Получить методы для обработки строк.
            WordProcessingHandler textCodeProcessor = GetMethods(request.TextCodeCommands);
            WordProcessingHandler unitProcessor = GetMethods(request.UnitCommands);

            var rateList = new List<ExchangeRate>(request.EndXpathRow - request.StartXpathRow + 1);
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

                rateList.Add(currencyData);
            }
            return rateList;
        }

        /// <summary>
        /// Получить методы обработки строки.
        /// </summary>
        /// <param name="methodNames">Наименование методов и соответствующие параметры.</param>
        /// <returns></returns>
        private static WordProcessingHandler GetMethods(IDictionary<string, string[]> methodNames)
        {
            if (methodNames != null)
            {
                WordProcessingHandler methods = null;
                // Получить тип объекта, содержащего методы.
                Type commandsType = typeof(Commands);
                // Создать объект, содержащий методы.
                object commandsObject = Activator.CreateInstance(commandsType);
                // Получить список методов.
                foreach (var methodName in methodNames)
                {
                    // Получить метод по заданному имени.
                    MethodInfo method = commandsType.GetMethod(methodName.Key);
                    var newMethod = method.Invoke(commandsObject, methodName.Value);
                    methods += newMethod as WordProcessingHandler;
                }
                return methods;
            }
            else
                // Вернуть метод, который возвращает целевую строку не обрабатывая ее.
                return text => text;
        }

        /// <summary>
        /// Получить текстовый код валюты по XPath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="html"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private static string GetTextCode(string xpath, HtmlDocument html, WordProcessingHandler handler)
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
        private static int GetUnit(string xpath, HtmlDocument html, WordProcessingHandler handler)
        {
            string unit = GetValueByXPath(html, xpath);
            unit = handler(unit);

            if (int.TryParse(unit, out int unitResult))
                return unitResult;
            else
                throw new Exception($"Ошибка при преобразовании строки единицы измерения {unit} в число.");
        }

        /// <summary>
        /// Получить обеменный курс валюты по XPath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="html"></param>
        /// <param name="decimalSeparator"></param>
        /// <param name="groupSeparator"></param>
        /// <returns></returns>
        private static decimal GetRate(string xpath, HtmlDocument html, string decimalSeparator, string groupSeparator)
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
                throw new Exception($"Ошибка при преобразовании строки обменного курса {exchangeRate} в число.");
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
            HtmlNode resultNode = html.DocumentNode.SelectSingleNode(xpath);

            if (resultNode != null)
            {
                var result = resultNode.InnerText;
                result = GetClearText(result);
                return result;
            }
            else
            {
                throw new Exception($"При получении данных по Xpath '{xpath}' полученно значение null.");
            }
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
        private static string GetActualXpath(string initialXpath, string variablePart, string variableValue) =>
            initialXpath.Replace(variablePart, variableValue);
    }
}

using HtmlAgilityPack;
using ParsingService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace ParsingService.Services
{
    /// <summary>
    /// Представляет средства для парсинга страницы банка.
    /// </summary>
    class ExchangeRatesService
    {
        public bool IsSuccessfullParsed => ErrorDictionaryService.IsValid;
        public Dictionary<string, IEnumerable<string>> ErrorDictionary => ErrorDictionaryService.ErrorDictionary;


        public ExchangeRatesService()
        {
            ErrorDictionaryService = new ErrorDictionaryService();
        }

        private IErrorDictionaryService ErrorDictionaryService { get; set; }

        /// <summary>
        /// Получить курсы валют банка асинхронно.
        /// </summary>
        /// <param name="request">Данные для запроса к банку.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ExchangeRate>> GetBankRatesAsync(BankRequest request)
        {
            HtmlDocument htmlDocument = null;
            var html = new HtmlWeb();
            try
            {
                htmlDocument = await html.LoadFromWebAsync(request.RatesUrl);
            }
            catch (Exception ex)
            {
                ErrorDictionaryService.AddError(MethodBase.GetCurrentMethod().Name, $"Ошибка при загрузке страницы {request.RatesUrl}: {ex.Message}");
                return Array.Empty<ExchangeRate>();
            }

            if (htmlDocument == null)
            {
                return Array.Empty<ExchangeRate>();
            }

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
        private WordProcessingHandler GetMethods(IDictionary<string, string[]> methodNames)
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
        private string GetTextCode(string xpath, HtmlDocument html, WordProcessingHandler handler)
        {
            string textCode = GetValueByXPath(html, xpath);
            try
            {
                textCode = handler(textCode);
            }
            catch (Exception ex)
            {
                ErrorDictionaryService.AddError(MethodBase.GetCurrentMethod().Name, $"Ошибка при выполнении команды обработки текста: {ex.Message}");
            }
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
            {
                return unitResult;
            }
            else
            {
                ErrorDictionaryService.AddError(MethodBase.GetCurrentMethod().Name, $"Ошибка при преобразовании строки '{unit}' в число.");
                return 0;
            }
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
            {
                return exchangeRateResult;
            }
            else
            {
                ErrorDictionaryService.AddError(MethodBase.GetCurrentMethod().Name, $"Ошибка при преобразовании строки '{exchangeRate}' в число.");
                return 0;
            }
        }

        /// <summary>
        /// Получить значение по адресу XPath.
        /// </summary>
        /// <param name="html">Страница для парсинга.</param>
        /// <param name="xpath">Адрес XPath искомого значения.</param>
        /// <returns></returns>
        private string GetValueByXPath(HtmlDocument html, string xpath)
        {
            // Узел целевого значения.
            HtmlNode resultNode = null;
            // Целевое значение.
            string result = "";            
            // Попытка получить узел.
            try
            {
                resultNode = html.DocumentNode.SelectSingleNode(xpath);
            }
            catch (System.Xml.XPath.XPathException)
            {
                ErrorDictionaryService.AddError(MethodBase.GetCurrentMethod().Name, $"Ошибка при получении данных по  Xpath '{xpath}'.");
                //throw new System.Xml.XPath.XPathException($"Ошибка при обработке XPath адреса {xpath}. ");
            }
            catch (ArgumentNullException)
            {
                ErrorDictionaryService.AddError(MethodBase.GetCurrentMethod().Name, $"Отсутствует Xpath '{xpath}'.");
                //throw new ArgumentNullException("Отсутствует XPath адрес");
            }

            if (resultNode != null)
            {
                result = resultNode.InnerText;
                // Привести форматирование текста к приемлемому виду.
                result = GetClearText(result);
            }
            else
                ErrorDictionaryService.AddError(MethodBase.GetCurrentMethod().Name, $"При получении данных по Xpath '{xpath}' полученно значение null.");
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

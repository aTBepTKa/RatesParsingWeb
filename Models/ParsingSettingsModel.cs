using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ParsingSettingsModel
    {
        /// <summary>
        /// XPath для текстового кода валюты.
        /// </summary>
        [DisplayName("XPath для текстового кода")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [MaxLength(2000, ErrorMessage = "Максимальная длина кода составляет 2000 символов")]
        public string TextCodeXpath { get; set; }

        /// <summary>
        /// XPath для единицы измерения валюты.
        /// </summary>
        [DisplayName("XPath для единицы измерения")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [MaxLength(2000, ErrorMessage = "Максимальная длина кода составляет 2000 символов")]
        public string UnitXpath { get; set; }

        /// <summary>
        /// XPath для значения обменного курса валюты.
        /// </summary>
        [DisplayName("Xpath для обменного курса")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [MaxLength(2000, ErrorMessage = "Максимальная длина кода составляет 2000 символов")]
        public string ExchangeRateXpath { get; set; }

        /// <summary>
        /// Изменяемая часть XPath.
        /// </summary>
        [DisplayName("Изменяемая часть XPath")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [MaxLength(50, ErrorMessage = "Максимальная длина кода составляет 50 символов")]
        public string VariablePartOfXpath { get; set; }

        /// <summary>
        /// Номер первой строки для парсинга.
        /// </summary>
        [DisplayName("Номер первой строки для парсинга")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public int StartXpathRow { get; set; }

        /// <summary>
        /// Номер последней строки для парсинга.
        /// </summary>
        [DisplayName("Номер последней строки для парсинга")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public int EndXpathRow { get; set; }

        /// <summary>
        /// Символ разделения десятичной части числа.
        /// </summary>
        [DisplayName("Разделитель десятичной части")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Разделитель представляется одиночным символом")]
        public string NumberDecimalSeparator { get; set; }

        /// <summary>
        /// Символ разделения групп разрядов числа.
        /// </summary>
        [DisplayName("Разделитель групп разрядов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Разделитель представляется одиночным символом")]
        public string NumberGroupSeparator { get; set; }
    }
}

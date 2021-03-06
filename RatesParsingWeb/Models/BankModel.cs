﻿using RatesParsingWeb.Models.ParsingSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class BankModel
    {
        public int Id { get; set; }

        /// <summary>
        /// SWIFT код банка.
        /// </summary>
        [DisplayName("SWIFT код банка")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Длина SWIFT кода составляет 11 символов.")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string SwiftCode { get; set; }

        /// <summary>
        /// Название банка.
        /// </summary>
        [DisplayName("Название банка")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [StringLength(50, ErrorMessage = "Максимальная длина названия не более 50 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на главную страницу банка.
        /// </summary>
        [DisplayName("Сайт банка")]
        [Url(ErrorMessage = "Необходимо ввести ссылку")]
        public string BankUrl { get; set; }

        /// <summary>
        /// Основная валюта банка.
        /// </summary>
        [DisplayName("Основная валюта банка")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public int CurrencyId { get; set; }

        /// <summary>
        /// Основная валюта банка.
        /// </summary>
        public CurrencyModel Currency { get; set; }

        public ParsingSettingsModel ParsingSettings { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class CurrencyModel
    {
        [DisplayName("Цифровой код валюты")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public int Id { get; set; }

        /// <summary>
        /// Наименование валюты.
        /// </summary>
        [DisplayName("Наименование валюты")]
        [StringLength(100, ErrorMessage = "Максимальная длина поля составляет 100 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Текстовый код валюты.
        /// </summary>
        [DisplayName("Код валюты")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [StringLength(3,MinimumLength =3,ErrorMessage = "Текстовый код валюты состоит из трех символов")]
        public string TextCode { get; set; }

        /// <summary>
        /// Код валюты с ее полным наименованием.
        /// </summary>
        [DisplayName("Наименование валюты")]
        public string CodeWithName =>
            $"{TextCode} - {Name}";


    }
}

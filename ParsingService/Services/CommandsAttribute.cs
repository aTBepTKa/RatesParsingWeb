using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingService.Services
{
    /// <summary>
    /// Описание команды.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class CommandAttribute : Attribute
    {
        /// <param name="name">Наименование команды.</param>
        /// <param name="description">Описание команды</param>
        public CommandAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Наименование команды.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание команды.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Описание параметра команды.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class ParameterAttribute : Attribute
    {
        /// <param name="name">Наименование параметра.</param>
        /// <param name="description">Описание параметра</param>
        public ParameterAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
        /// <summary>
        /// Наименование параметра.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание параметра.
        /// </summary>
        public string Description { get; set; }
    }
}

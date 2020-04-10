using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingMessages.Command
{
    public interface ICommandParameter
    {
        /// <summary>
        /// Наименование параметра.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Описание параметра.
        /// </summary>
        string Description { get; set; }
    }
}

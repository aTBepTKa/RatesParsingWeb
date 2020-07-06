using ParsingMessages.Command;

namespace RatesParsingWeb.Dto.CommandService
{
    /// <summary>
    /// Параметр, полученный из внешнего сервиса.
    /// </summary>
    public class ExternalParameterDto : ICommandParameter
    {
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

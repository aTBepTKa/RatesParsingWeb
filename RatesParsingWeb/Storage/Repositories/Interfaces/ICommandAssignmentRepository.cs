using RatesParsingWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories.Interfaces
{
    public interface ICommandAssignmentRepository : IRepository<CommandAssignment>
    {
        /// <summary>
        /// Получить команду с параметрами и их значениями.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommandAssignment GetWithParameters(int id);
    }
}

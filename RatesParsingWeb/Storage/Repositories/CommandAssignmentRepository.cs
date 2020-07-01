using Microsoft.EntityFrameworkCore;
using RatesParsingWeb.Domain;
using RatesParsingWeb.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Storage.Repositories
{
    public class CommandAssignmentRepository : RepositoryBase<CommandAssignment>, ICommandAssignmentRepository
    {
        public CommandAssignmentRepository(BankRatesContext context) : base(context)
        {
        }

        public CommandAssignment GetWithParameters(int id)
        {
            var command = dbSet
                .Include(assignment => assignment.AssignmentFieldName)
                .Include(assignment => assignment.Command)
                    .ThenInclude(command => command.CommandParameters)
                .Include(assignment => assignment.CommandParameterValues)
                    .ThenInclude(parameterValue => parameterValue.CommandParameter)
                .FirstOrDefault(assignment => assignment.Id == id);
            return command;
        }
    }
}

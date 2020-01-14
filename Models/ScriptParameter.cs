using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ScriptParameter
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ScriptAssignmentID { get; set; }

        public ScriptAssignment ScriptAssignment { get; set; }
    }
}

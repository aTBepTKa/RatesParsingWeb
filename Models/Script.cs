using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class Script
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParamsNum { get; set; }

        public ICollection<ScriptAssignment> ScriptAssignments { get; set; }
    }
}

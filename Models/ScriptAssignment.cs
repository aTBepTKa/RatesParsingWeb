using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatesParsingWeb.Models
{
    public class ScriptAssignment
    {
        public int ID { get; set; }
        public int ScriptID { get; set; }
        public int BankID { get; set; }

        public Script Script { get; set; }
        public Bank Bank { get; set; }
        public ICollection<ScriptParameter> ScriptParameters { get; set; }
    }
}

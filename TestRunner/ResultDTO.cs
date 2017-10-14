using System;
using System.Collections.Generic;
using System.Text;

namespace TestRunner
{
    class ResultDTO
    {
        public String Name { get; set; }
        public String Outcome { get; set; }
        public String ErrorMessage { get; set; }
        public String StackTrace { get; set; }
    }
}

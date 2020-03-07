using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.NEWSHIT.CUSTOM_COMMANDS
{
    public class CustomCommand
    {
        public string Name { get; set; }

        public List<string> Alias = new List<string>();

        public string Response { get; set; }

        public CustomCommand(string name, string response, List<string> alias = null)
        {
            Name = name;
            Response = response;
            if (Alias != null) Alias = alias;
        }
    }
}

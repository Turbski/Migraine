using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.ADDITIONS.CUSTOM_COMMANDS
{
    public class CustomCommand
    {
        public string Name { get; set; }

        public string Response { get; set; }

        public CustomCommand(string name, string response)
        {
            Name = name;
            Response = response;
        }
    }
}

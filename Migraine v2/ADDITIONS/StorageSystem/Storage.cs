using Migraine_v2.ADDITIONS.CUSTOM_COMMANDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.ADDITIONS.StorageSystem
{
    public class Storage
    {
        public List<CustomCommand> CustomCmds = new List<CustomCommand>();

        public Storage()
        {
            CustomCmds.Add(new CustomCommand("test", "testing ok"));
        }
    }
}

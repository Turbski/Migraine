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
        public List<CustomCommand> CustomCmds = new List<CustomCommand>()
        {
            new CustomCommand("test", "This is the default custom command! (Added for testing purposes, of course)", new List<string>() { "testing", "test2" })
        };
    }
}

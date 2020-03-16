using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.API.Gateway
{
    public class GatewayResponse
    {
        public int Opcode { get; private set; }

        public string Title { get; private set; }
        public object Data { get; set; }
        public uint? Sequence { get; set; }
    }
}

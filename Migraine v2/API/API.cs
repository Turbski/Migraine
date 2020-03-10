using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.API
{
    class API
    {
        public class RootObject
        {
            public string url { get; set; }
            public string image { get; set; }
        }
        public class RandomHentai
        {
            public class RootObject
            {
                public string url { get; set; }
                public string image { get; set; }
            }
        }
        public class RandomHug
        {
            public class RootObject
            {
                public string url { get; set; }
            }
        }
    }
}

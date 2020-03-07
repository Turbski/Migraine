using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.ADDITIONS.StorageSystem
{
    public static class StorageHandler
    {
        public static Storage _Storage { get; set; }

        public static void LoadStorage()
        {
            if (!File.Exists("storage.json"))
            {
                File.WriteAllText("storage.json", JsonConvert.SerializeObject(new Storage()));
            }

            _Storage = JsonConvert.DeserializeObject<Storage>(File.ReadAllText("storage.json"));
        }

        public static void SaveStorage() => File.WriteAllText("storage.json", JsonConvert.SerializeObject(_Storage));
    }
}

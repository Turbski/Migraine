using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.CustomSettings
{
    public static class Configuration
    {
        public static Config _Config { get; set; }

        public static void LoadConfig()
        {
            if (!File.Exists("Settings.json"))
            {
                File.WriteAllText("Settings.json", JsonConvert.SerializeObject(new Config()));
            }

            _Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Settings.json"));
        }

        public static void SaveConfig() => File.WriteAllText("Settings.json", JsonConvert.SerializeObject(_Config));
    }

    public class Config
    {
        public Dictionary<string, string> CustomCommands = new Dictionary<string, string>();
    }
}

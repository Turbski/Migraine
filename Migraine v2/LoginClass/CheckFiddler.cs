using System.Diagnostics;
using System.Windows.Forms;
namespace Migraine_v2.LoginClass {
    class CheckFiddler {
        public static bool _()
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                Process process = processes[i];
                if (process.ProcessName.ToLower().Contains("fiddler") || process.ProcessName.ToLower().Contains("dnspy") || process.ProcessName.ToLower().Contains("wireshark") || process.ProcessName.ToLower().Contains("postman") || process.ProcessName.ToLower().Contains("megadumper") || process.ProcessName.ToLower().Contains("simpleassembly") || process.ProcessName.ToLower().Contains("httpanalyzer") || process.ProcessName.ToLower().Contains("httpdebug") || process.ProcessName.ToLower().Contains("proxifier") || process.ProcessName.ToLower().Contains("mitmproxy") || process.ProcessName.ToLower().Contains("charles") || process.ProcessName.ToLower().Contains("peek") || process.ProcessName.ToLower().Contains("unpack") || process.ProcessName.ToLower().Contains("appfusc") || process.ProcessName.ToLower().Contains("confus") || process.ProcessName.ToLower().Contains("eazdevirt") || process.ProcessName.ToLower().Contains("eazfixer") || process.ProcessName.ToLower().Contains("ilprotector") || process.ProcessName.ToLower().Contains("nofuserex") || process.ProcessName.ToLower().Contains("peid") || process.ProcessName.ToLower().Contains("ollydump") || process.ProcessName.ToLower().Contains("processexp") || process.ProcessName.ToLower().Contains("cheatengine-x86_64") || process.ProcessName.ToLower().Contains("Auth Bypass") || process.ProcessName.ToLower().Contains("patcher") || process.ProcessName.ToLower().Contains("Auth Patcher"))
                {
                    MessageBox.Show("Skid, Remove ur skid tools please.", "Spytool Detected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return true;
                }
            }
            return false;
        }
    }
}

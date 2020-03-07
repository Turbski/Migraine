using System.Management;
namespace Migraine_v2.LoginClass
{
    public class HardwareID {
        public static string GET() {
            return $"{CpuId()}{DiskId()}";
        }
        public static string CpuId() {
            string str = null;
            ManagementObjectCollection managementObjectCollections = (new ManagementObjectSearcher("Select ProcessorId From Win32_processor")).Get();
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollections.GetEnumerator();
                if (enumerator.MoveNext())
                    str = ((ManagementObject)enumerator.Current)["ProcessorId"].ToString();

            return str;
        }
        public static string DiskId() {
            ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"C:\"");
            managementObject.Get();
            return managementObject["VolumeSerialNumber"].ToString();
        }
    }
}

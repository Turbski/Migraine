using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.Client.Asar_Extraction
{
    public class Utilities
    {
        public static void WriteFile(byte[] bytes, String destination)
        {
            String dirPath = Path.GetDirectoryName(destination);
            String filename = Path.GetFileName(destination);

            Directory.CreateDirectory(dirPath);

            File.WriteAllBytes(destination, bytes);
        }

        public static void CreateDirectory(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

    }
}

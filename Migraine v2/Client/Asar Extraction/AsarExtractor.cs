using Migraine_v2.Client.Asar_Extraction;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.Client
{
    public class AsarExtractor
    {

        public Boolean Extract(AsarArchive archive, String filepath, String destination)
        {
            String[] path = filepath.Split('/');

            JToken token = archive.GetHeader().GetHeaderJson();

            for (int i = 0; i < path.Length; i++)
            {
                token = token["files"][path[i]];
            }

            int size = token.Value<int>("size");
            int offset = archive.GetBaseOffset() + token.Value<int>("offset");

            byte[] fileBytes = archive.GetBytes().Skip(offset).Take(size).ToArray();

            Utilities.WriteFile(fileBytes, destination);

            return false;
        }

        private List<AFile> _filesToExtract;
        private bool _emptyDir = false;

        public Boolean ExtractAll(AsarArchive archive, String destination, bool emptyDir = false)
        {
            _filesToExtract = new List<AFile>();

            _emptyDir = emptyDir;

            JObject jObject = archive.GetHeader().GetHeaderJson();
            if (jObject.HasValues)
                TokenIterator(jObject.First);

            byte[] bytes = archive.GetBytes();

            foreach (AFile aFile in _filesToExtract)
            {
                int size = aFile.GetSize();
                int offset = archive.GetBaseOffset() + aFile.GetOffset();
                if (size > -1)
                {
                    byte[] fileBytes = new byte[size];

                    Buffer.BlockCopy(bytes, offset, fileBytes, 0, size);
                    Utilities.WriteFile(fileBytes, destination + aFile.GetPath());
                }
                else
                {
                    if (_emptyDir)
                        Utilities.CreateDirectory(destination + aFile.GetPath());
                }
            }

            return false;
        }

        private struct AFile
        {
            private String _path;
            public String GetPath() { return _path; }
            private int _size;
            public int GetSize() { return _size; }
            private int _offset;
            public int GetOffset() { return _offset; }

            public AFile(String path, String fileName, int size, int offset)
            {
                path = path.Replace("['", "").Replace("']", "");
                path = path.Substring(0, path.Length - fileName.Length);
                path = path.Replace(".files.", "/").Replace("files.", "");
                path += fileName;

                _path = path;
                _size = size;
                _offset = offset;
            }
        }

        private void TokenIterator(JToken jToken)
        {
            JProperty jProperty = jToken as JProperty;

            foreach (JProperty prop in jProperty.Value.Children())
            {
                int size = -1;
                int offset = -1;
                foreach (JProperty nextProp in prop.Value.Children())
                {
                    if (nextProp.Name == "files")
                    {
                        if (_emptyDir)
                        {
                            AFile afile = new AFile(prop.Path, "", size, offset);
                            _filesToExtract.Add(afile);
                        }

                        TokenIterator(nextProp);
                    }
                    else
                    {
                        if (nextProp.Name == "size")
                            size = Int32.Parse(nextProp.Value.ToString());
                        if (nextProp.Name == "offset")
                            offset = Int32.Parse(nextProp.Value.ToString());
                    }
                }

                if (size > -1 && offset > -1)
                {
                    AFile afile = new AFile(prop.Path, prop.Name, size, offset);
                    _filesToExtract.Add(afile);
                }
            }
        }
    }
}

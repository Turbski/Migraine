using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.Client.Asar_Extraction
{
    public class AsarArchive
    {
        private const int SIZE_UINT = 4;

        private readonly int _baseOffset;
        public int GetBaseOffset() { return _baseOffset; }

        private readonly byte[] _bytes;
        public byte[] GetBytes() { return _bytes; }

        private readonly String _filePath;
        public String GetFilePath() { return _filePath; }

        private Header _header;
        public Header GetHeader() { return _header; }

        public struct Header
        {
            private readonly byte[] _headerInfo;
            public byte[] GetHeaderInfo() { return _headerInfo; }
            private readonly int _headerLength;
            public int GetHeaderLenth() { return _headerLength; }

            private readonly byte[] _headerData;
            public byte[] GetHeaderData() { return _headerData; }
            private readonly JObject _headerJson;
            public JObject GetHeaderJson() { return _headerJson; }

            public Header(byte[] hinfo, int length, byte[] data, JObject hjson)
            {
                _headerInfo = hinfo;
                _headerLength = length;
                _headerData = data;
                _headerJson = hjson;
            }
        }

        public AsarArchive(String filePath)
        {
            if (!File.Exists(filePath))
                throw new AsarExceptions(AsarException.ASAR_FILE_CANT_FIND);

            _filePath = filePath;

            try
            {
                _bytes = File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                throw new AsarExceptions(AsarException.ASAR_FILE_CANT_READ, ex.ToString());
            }

            try
            {
                _header = ReadAsarHeader(ref _bytes);
                _baseOffset = _header.GetHeaderLenth();
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        private static Header ReadAsarHeader(ref byte[] bytes)
        {
            int SIZE_LONG = 2 * SIZE_UINT;
            int SIZE_INFO = 2 * SIZE_LONG;

            byte[] headerInfo = bytes.Take(SIZE_INFO).ToArray();

            if (headerInfo.Length < SIZE_INFO)
                throw new AsarExceptions(AsarException.ASAR_INVALID_FILE_SIZE);

            byte[] asarFileDescriptor = headerInfo.Take(SIZE_LONG).ToArray();
            byte[] asarPayloadSize = asarFileDescriptor.Take(SIZE_UINT).ToArray();

            int payloadSize = BitConverter.ToInt32(asarPayloadSize, 0);
            int payloadOffset = asarFileDescriptor.Length - payloadSize;

            if (payloadSize != SIZE_UINT && payloadSize != SIZE_LONG)
                throw new AsarExceptions(AsarException.ASAR_INVALID_DESCRIPTOR);

            byte[] asarHeaderLength = asarFileDescriptor.Skip(payloadOffset).Take(SIZE_UINT).ToArray();

            int headerLength = BitConverter.ToInt32(asarHeaderLength, 0);

            byte[] asarFileHeader = headerInfo.Skip(SIZE_LONG).Take(SIZE_LONG).ToArray();
            byte[] asarHeaderPayloadSize = asarFileHeader.Take(SIZE_UINT).ToArray();

            int headerPayloadSize = BitConverter.ToInt32(asarHeaderPayloadSize, 0);
            int headerPayloadOffset = headerLength - headerPayloadSize;

            byte[] dataTableLength = asarFileHeader.Skip(headerPayloadOffset).Take(SIZE_UINT).ToArray();
            int dataTableSize = BitConverter.ToInt32(dataTableLength, 0);

            byte[] hdata = bytes.Skip(SIZE_INFO).Take(dataTableSize).ToArray();

            if (hdata.Length != dataTableSize)
                throw new AsarExceptions(AsarException.ASAR_INVALID_FILE_SIZE);

            int asarDataOffset = asarFileDescriptor.Length + headerLength;

            JObject jObject = JObject.Parse(System.Text.Encoding.Default.GetString(hdata));

            return new Header(headerInfo, asarDataOffset, hdata, jObject);
        }
    }
}

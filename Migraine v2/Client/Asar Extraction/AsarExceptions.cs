using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.Client.Asar_Extraction
{
    public enum AsarException
    {
        ASAR_FILE_CANT_FIND,
        ASAR_FILE_CANT_READ,
        ASAR_INVALID_DESCRIPTOR,
        ASAR_INVALID_FILE_SIZE
    };

    public class AsarExceptions : Exception
    {
        private readonly AsarException _asarException;
        private readonly string _asarMessage;

        public AsarExceptions(AsarException ex) : this(ex, "") { }

        public AsarExceptions(AsarException ex, String customMessage)
        {
            _asarException = ex;
            if (customMessage.Length > 0)
                _asarMessage = customMessage;
            else
                _asarMessage = GetMessage(ex);
        }

        private String GetMessage(AsarException ex)
        {
            String result;

            switch (ex)
            {
                case AsarException.ASAR_FILE_CANT_FIND:
                    result = "Error: The specified file couldn't be found.";
                    break;
                case AsarException.ASAR_FILE_CANT_READ:
                    result = "Error: File can't be read.";
                    break;
                case AsarException.ASAR_INVALID_DESCRIPTOR:
                    result = "Error: File's header size is not defined on 4 or 8 bytes.";
                    break;
                case AsarException.ASAR_INVALID_FILE_SIZE:
                    result = "Error: Data table size shorter than the size specified in in the header.";
                    break;
                default:
                    result = "Error: Unhandled exception!";
                    break;
            }

            return result;
        }

        public AsarException GetExceptionCode()
        {
            return _asarException;
        }

        public String GetExceptionMessage()
        {
            return _asarMessage;
        }

        override public String ToString()
        {
            return "(Code " + GetExceptionCode() + ") " + GetExceptionMessage();
        }
    }
}

using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ust.Api.Common
{
    public class UstApplicationException : Exception
    {
        public ErrorCode Code { get; }

        public UstApplicationException(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                return;
            }

            Code = (ErrorCode)Enum.Parse(typeof(ErrorCode), info.GetString("Type").Split(':').Last());
        }

        public UstApplicationException(ErrorCode code)
        {
            Code = code;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorCode
    {
        NewsNotFound,
        FileNotFound
    }
}

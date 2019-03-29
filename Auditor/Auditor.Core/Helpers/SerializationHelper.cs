using Newtonsoft.Json;
using System;

namespace Auditor.Core.Helpers
{
    public static class SerializationHelper
    {
        private static JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        public static string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, Formatting.None, _serializerSettings);
        }

        public static T Unserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
        public static object Unserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }
    }
}
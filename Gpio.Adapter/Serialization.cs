using Gpio.Abstract;
using System.Collections.Generic;
using Newtonsoft.Json;
using Gpio.Implemention;

namespace Gpio.Adapter
{
    public static class Serialization
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        public static string Serialize(Dictionary<short, Pin> pinPair)
        {
            return JsonConvert.SerializeObject(pinPair, _settings);
        }

        public static Dictionary<short, Pin> DeSerialize(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<short, Pin>>(data, _settings);
        }
    }
}

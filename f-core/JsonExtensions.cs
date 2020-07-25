using Newtonsoft.Json;

namespace f_core
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T self)
        {
            var json = JsonConvert.SerializeObject(self);
            return json;
        }

        public static T Parse<T>(this string json)
        {
            var self = JsonConvert.DeserializeObject<T>(json);
            return self;
        }
    }
}

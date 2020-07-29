using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

[assembly: InternalsVisibleTo("f-nunit")]

namespace f_core
{
    public static class Helpers
    {
        public static string ToJson<T>(this T self)
        {
            var json = JsonConvert.SerializeObject(self);
            return json;
        }

        public static T Parse<T>(this string json, T defaults = null)
            where T: class
        {
            if (json.IsEmpty())
                return null;

            if (defaults == null)
                return JsonConvert.DeserializeObject<T>(json);

            var jSelf = JObject.FromObject(defaults);

            jSelf.Merge(JObject.Parse(json));

            var self = jSelf.ToObject<T>();

            return self;
        }

        public static bool IsEmpty(this string str)
        {
            return
                string.IsNullOrEmpty(str);
        }

        public static bool IsNotEmpty(this string str)
        {
            return
                !string.IsNullOrEmpty(str);
        }

        public static string AsSha(this string str)
        {
            return str;
        }

        private static readonly Random _rand = new Random();

        public static string MakeTrackingId()
        {
            string makeSegment(int max) =>
                _rand.Next(max).ToString("00");

            var s1 = makeSegment(100);
            var s2 = makeSegment(100);
            var s3 = makeSegment(100);

            return
                $"{s1}.{s2}.{s3}";
        }
    }
}

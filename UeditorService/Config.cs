using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace UeditorService
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public static class Config
    {
        private static bool noCache = true;
        private static JObject BuildItems()
        {
            var rootDir = Environment.CurrentDirectory;
            var json = File.ReadAllText(rootDir + "/Config/Ueditor.json");
            return JObject.Parse(json);
        }



        public static JObject Items
        {
            get
            {


                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }

        public static string ItemsStr
        {
            get
            {


                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return JsonConvert.SerializeObject(_Items);
            }
        }

        private static JObject _Items;


        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}

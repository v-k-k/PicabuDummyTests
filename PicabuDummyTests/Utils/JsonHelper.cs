using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace PicabuDummyTests.Utils
{
    public static class JsonHelper
    {
        public static Dictionary<string, string> Deserialize(string source)
        {
            using (FileStream fs = new FileStream(source, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
                }
            }
        }
    }
}

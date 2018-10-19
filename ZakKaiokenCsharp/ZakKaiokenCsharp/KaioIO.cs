using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaiosharp
{
    public class KaioIO
    {
        static private string jsonx;

        public static void saveArray<t>(t array)
        {

            jsonx = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(@".\Settings.json", jsonx);
        }

        public static t LoadArray<t>(t array)
        {
            return JsonConvert.DeserializeObject<t>(File.ReadAllText(@".\Settings.json"));
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZomBot.Models;

namespace ZomBot
{
    class Configuration
    {
        public static ConfigModel Config = new ConfigModel();

        public static void SetupConfig()
        {
            var json = "";
            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = sr.ReadToEnd();
                }
            }
            Config = JsonConvert.DeserializeObject<ConfigModel>(json);
        }
    }
}

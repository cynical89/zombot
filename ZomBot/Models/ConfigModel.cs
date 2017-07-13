using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZomBot.Models
{
    class ConfigModel
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("cmdPrefix")]
        public string CommandPrefix { get; private set; }

        [JsonProperty("ytkey")]
        public string YoutubeKey { get; private set; }
    }
}

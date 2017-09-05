using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject
{
    public class EpisodeJson
    {
        [JsonProperty("episode")] // "Root-klassen" EpisodeJson behövs pga json-strängen som returneras är nästlad
        public Episode Episode { get; set; }
    }
}

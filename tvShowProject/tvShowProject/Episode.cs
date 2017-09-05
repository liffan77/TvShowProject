using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject
{
    public class Episode
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("release_date")]
        public string Release_Date { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("show")]
        public Show Show { get; set; }
    }
}

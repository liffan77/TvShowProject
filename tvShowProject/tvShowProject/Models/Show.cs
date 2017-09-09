using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tvShowProject.Models;

namespace tvShowProject
{
    public class Show
    {
        public int Id { get; set; }
        public string Url { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Title { get; set; }
        public string[] Genres { get; set; }
        public Rating Rating { get; set; }
        public string Premiered { get; set; }
        public string OfficialSite { get; set; }
        [JsonProperty(PropertyName = "externals")]
        public Externals ExternalIds { get; set; }
        [JsonProperty(PropertyName = "image")]
        public Image ImageUrls { get; set; }
        public string Summary { get; set; }
        public Episode[] Episodes { get; set; }
    }


}


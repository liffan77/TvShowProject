using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject
{
    public class Show
    {
        //public string Title { get; set; }
        //public string Imdb_Id { get; set; }
        //public string Epguide_Name { get; set; }

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

    }

    public class Image
    {
        public string Medium { get; set; }
        public string Original { get; set; }
    }

    public class Externals
    {
        public int? TvRage { get; set; }
        public int? TheTvDb { get; set; }
        public string Imdb { get; set; }
    }

    public class Rating
    {
        public float? Average { get; set; }
    }
}


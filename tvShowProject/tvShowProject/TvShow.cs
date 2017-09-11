using Newtonsoft.Json;
using System;

namespace tvShowProject
{
    public class TvShow
    {
        public int Id { get; set; }
        public Uri Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string[] Genres { get; set; }
        public string Status { get; set; }
        public int? Runtime { get; set; }
        public DateTime? Premiered { get; set; }
        public Rating Rating { get; set; }
        public int Weight { get; set; }
        public Externals Externals { get; set; }
        public Image Image { get; set; }
        public string Summary { get; set; }
        [JsonProperty(PropertyName = "_embedded")]
        public EmbeddedItems EmbeddedItems { get; set; }

        public void GetEmbedded()
        {
            EmbeddedItems.Episodes = ApiHandler.GetEmbeddedEpisodes(Id);
        }
    }
}
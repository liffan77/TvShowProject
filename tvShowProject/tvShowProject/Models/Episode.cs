using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject.Models
{
    public class Episode
    {
        public uint Id { get; set; }
        public Uri Url { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public DateTime? Airdate { get; set; }
        public DateTimeOffset? Airstamp { get; set; }
        public int Runtime { get; set; }
        public Image Image { get; set; }
        public string Summary { get; set; }
        public Show Show { get; set; }
    }
}

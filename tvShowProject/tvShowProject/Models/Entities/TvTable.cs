using System;
using System.Collections.Generic;

namespace tvShowProject.Models.Entities
{
    public partial class TvTable
    {
        public TvTable()
        {
            UserToTvTable = new HashSet<UserToTvTable>();
        }

        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string QueryString { get; set; }
        public DateTime? NextReleaseDate { get; set; }

        public ICollection<UserToTvTable> UserToTvTable { get; set; }
    }
}

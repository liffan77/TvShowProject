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
        public string Title { get; set; }
        public int? TvMazeId { get; set; }

        public ICollection<UserToTvTable> UserToTvTable { get; set; }
    }
}

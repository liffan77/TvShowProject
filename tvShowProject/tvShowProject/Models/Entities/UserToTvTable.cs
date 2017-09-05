using System;
using System.Collections.Generic;

namespace tvShowProject.Models.Entities
{
    public partial class UserToTvTable
    {
        public int UserId { get; set; }
        public int TvTableId { get; set; }

        public TvTable TvTable { get; set; }
        public User User { get; set; }
    }
}

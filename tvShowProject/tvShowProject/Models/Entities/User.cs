using System;
using System.Collections.Generic;

namespace tvShowProject.Models.Entities
{
    public partial class User
    {
        public User()
        {
            UserToTvTable = new HashSet<UserToTvTable>();
        }

        public int Id { get; set; }
        public string AspNetUserId { get; set; }

        public ICollection<UserToTvTable> UserToTvTable { get; set; }
    }
}

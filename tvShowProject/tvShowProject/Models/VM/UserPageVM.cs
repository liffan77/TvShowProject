﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject.Models.VM
{
    public class UserPageVM
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string SearchString { get; set; }
        public SearchResultItemVM[] SearchResultItems { get; set; }
        public FollowedShowItemVM[] FollowedShowItems { get; set; }
    }
}

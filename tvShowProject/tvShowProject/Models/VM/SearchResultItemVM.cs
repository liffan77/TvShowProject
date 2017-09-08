using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject.Models.VM
{
    [Bind(Prefix = nameof(UserPageVM.SearchResultItems))]
    public class SearchResultItemVM
    {
        public string Title { get; set; }
        public string ImdbId { get; set; }
    }
}

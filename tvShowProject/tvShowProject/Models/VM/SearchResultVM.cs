using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject.Models.VM
{
    //public class SearchResultVM
    //{
    //    public List<Show> Shows { get; set; }
    //}

    public class SearchResultVM
    {
        public float Score { get; set; }
        public Show Show { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject.Models.Entities
{
    public partial class TvContext
    {
        public TvContext(DbContextOptions<TvContext> options)
            : base(options)
        {
            
        }
        
        public async Task AddUser(string aspNetUserId)
        {
            // lägg till i DB
            User.Add(new Entities.User { AspNetUserId = aspNetUserId });
            await SaveChangesAsync();
        }
    }
}

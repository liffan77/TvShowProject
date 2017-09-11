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
            User.Add(new Entities.User { AspNetUserId = aspNetUserId });
            await SaveChangesAsync();
        }

        public void UnFollowDB(int tvMazeId, int userId)
        {
            // ta fram TvTableId för att kunna ta bort kopplingen i mellanliggande DB
            var tvTableId = TvTable
                .SingleOrDefault(i => i.TvMazeId == tvMazeId).Id;

            // hämta raden som ska tas bort från UserToTvTable
            var tmp = UserToTvTable
                .SingleOrDefault(x => x.UserId == userId && x.TvTableId == tvTableId);

            UserToTvTable.Remove(tmp);
            SaveChanges();
        }
        
    }
}

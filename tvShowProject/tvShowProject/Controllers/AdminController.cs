using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using tvShowProject.Models.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tvShowProject.Controllers
{
    public class AdminController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        IdentityDbContext _identityContext;
        TvContext _tvContext;

        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IdentityDbContext identityContext, TvContext tvContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;
            _tvContext = tvContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckForNewEpisodes()
        {
            List<int?> releasedTodayMazeIds = new List<int?>();
            List<int> userIdsToNotify = new List<int>();

            // get all tvMazeId's to search for
            var tvMazeIds = _tvContext.TvTable
                .Select( x => x.TvMazeId).ToArray();

            // check whether any shows have released an episode today
            foreach (var id in tvMazeIds)
            {
                // get the latest episode for each show
                Episode e = ApiHandler.GetLatestEpisode(id);
                // check if the latest episodes date is today
                if (e.Airdate.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                {
                    releasedTodayMazeIds.Add(id);
                    // notify the users();
                }

            }

            // då har vi en lista med alla tvShows vars användare ska notifieras
            foreach (var mazeId in releasedTodayMazeIds)
            {
                // plocka ut alla användare som följer just denna serien
                var usersThatFollow = _tvContext.UserToTvTable
                    .Where(i => i.TvTableId == mazeId)
                    .Select(u => u.UserId);


                

                // ta ut relevant info om serien1 o skicka notis till u1 user och u2 user

                
            }

            

            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using tvShowProject.Models.VM;
using tvShowProject.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tvShowProject.Controllers
{
    public class ShowsController : Controller
    {
        TvContext _tvContext;
        IdentityDbContext _identityContext;
        UserManager<IdentityUser> _userManager;


        public ShowsController(TvContext tvContext, IdentityDbContext identityDbContext, UserManager<IdentityUser> userManager)
        {
            _tvContext = tvContext;
            _identityContext = identityDbContext;
            _userManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Authorize] //Lägg överst för att kräva en inloggad session för att komma åt sidorna.
        [HttpGet]
        public IActionResult UserPage()
        {
            UserPageVM userPageVM = new UserPageVM();
            userPageVM.Username = User.Identity.Name;

            // hämta id:t i aspNet
            var aspNetId = _userManager
                .GetUserId(HttpContext.User);

            // hämta id:t i tvContext
            var userId = _tvContext
                .User
                .FirstOrDefault(i => i.AspNetUserId == aspNetId)
                .Id;

            // hämta usern
            var user = _tvContext
                .UserToTvTable
                .Where(x => x.UserId == userId);

            // hämta array av FollowedShowItem
            var followedShowItems = user
                .Select(x => new FollowedShowItemVM
                {
                    Title = x.TvTable.Title,
                    ImdbId = x.TvTable.ImdbId
                })
                .ToArray();

            userPageVM.FollowedShowItems = followedShowItems;

            return View(userPageVM);
        }

        [HttpGet]
        public IActionResult ShowDetails(ShowDetailsVM showDetailsVM)
        {
            // Visa vymodellens information
            return null;
        }

        [HttpPost]
        public IActionResult ShowDetails()
        {
            // anropa API_handler
            // få datan från api-handler, skapa en ShowDetailsVM som skickas till Get

            return null;
        }

        [HttpGet]
        public IActionResult Search(SearchResultItemVM[] searchResultItemsVM)
        {

            // visar resultatet
            return null;
        }

        [HttpPost]
        public IActionResult Search(UserPageVM userPageVM)
        {
            if (!ModelState.IsValid)
                return View(userPageVM);

            ApiHandler apiHandler = new ApiHandler();
            string responseString = apiHandler.ShowSearch(userPageVM.SearchString);

            SearchResultVM[] result = JsonConvert.DeserializeObject<SearchResultVM[]>(responseString);


            return View(result);
        }


    }
}

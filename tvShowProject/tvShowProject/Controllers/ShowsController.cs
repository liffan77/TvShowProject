﻿using System;
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

            int userId = GetUserId();

            // hämta usern
            var user = _tvContext
                .UserToTvTable
                .Where(x => x.UserId == userId);

            // hämta array av FollowedShowItem
            var followedShowItems = user
                .Select(x => new FollowedShowItemVM
                {
                    Title = x.TvTable.Title,
                    Id = x.TvTable.TvMazeId,
                    // använd apihandler för att sätta bool-proppen is released
                    ReleasedToday = ApiHandler.CheckIfAnyEpisodeReleasedToday(x.TvTable.TvMazeId)
                })
                .ToArray();

            userPageVM.FollowedShowItems = followedShowItems;

            return View(userPageVM);
        }


        [HttpGet]
        public IActionResult ShowDetails(int id)
        {

            if (!ModelState.IsValid)
                return View();


            TvShow tvShow = ApiHandler.GetTvShowAndEpisodeDetails(id);
            ShowDetailsVM showDetailsVm = new ShowDetailsVM
            {
                Id = tvShow.Id,
                Title = tvShow.Name,
                Summary = tvShow.Summary,
                Episodes = tvShow.EmbeddedItems.Episodes,
                ImageUrls = tvShow.Image
            };
            
            return PartialView("ShowDetails", showDetailsVm);
        }

        //var viewModel = dm.GetCatViewModel(id);

        //    return PartialView("CatBox", viewModel);

        // funkar itne ännu
        [HttpPost]
        public IActionResult ShowDetails(string imdbId, int deleteMe)
        {
            //// hårdkodat för testning
            //imdbId = "tt0944947";
            //// end hårdkodat

            //if (!ModelState.IsValid)
            //    return View();

            //// anropa API_handler
            //// få datan från api-handler, skapa en ShowDetailsVM som skickas till Get
            //ApiHandler apiHandler = new ApiHandler();
            //string responseString = apiHandler.GetShowDetails(imdbId);

            //ShowDetailsVM showDetailsVm = JsonConvert.DeserializeObject<ShowDetailsVM>(responseString);

            return View();
        }

        //Tror itne denna action behövs, eftersom vi aldrig gettar search
        [HttpGet]
        //public IActionResult Search(SearchResultItemVM[] searchResultItems)
            public IActionResult Search(string searchString)
        {

            // visar resultatet
            //return null;

            SearchResult[] searchResult = ApiHandler.SearchForShow(searchString);

            return PartialView("Search", searchResult);
        }


        //[HttpPost]
        //public IActionResult Search(UserPageVM userPageVM)
        //{
        //    if (!ModelState.IsValid)
        //        return View(userPageVM);

        //    SearchResult[] searchResult = ApiHandler.SearchForShow(userPageVM.SearchString);

        //    return PartialView("Search", searchResult);
        //}

        [HttpPost]
        public IActionResult Follow(int id, string title)
        {
            //todo banta ner controllers, lägg logiken i tvContext

            //todo visa bekräftelse

            // add to DB
            // lägg till i tvTable OM den inte finns
            // finns id i tvTable?

            // fråga DB, finns detta IMDB-id redan?
            var tvTable = _tvContext.TvTable
                .SingleOrDefault(s => s.TvMazeId == id);

            // om showen inte fanns, nya upp den och spara i DB
            if (tvTable == null)
            {
                // nya upp en entitet
                tvTable = new TvTable
                {
                    TvMazeId = id,
                    Title = title
                };

                // lägg till den nya entiteten till DB
                _tvContext.TvTable.Add(tvTable);
                _tvContext.SaveChanges(); // måste spara här för att få ett ID
            }

            // kolla att användaren inte redan följer serien
            int userId = GetUserId();
            var tmp2 = _tvContext.UserToTvTable
                .SingleOrDefault(u => u.TvTableId == tvTable.Id && u.UserId == userId);

            // om användaren ej redan följer serien
            if (tmp2 == null)
            {
                // skapa ny post i UserToTvTable DB
                UserToTvTable userToTvTable = new UserToTvTable
                {
                    TvTableId = tvTable.Id,
                    UserId = userId,
                };

                _tvContext.UserToTvTable.Add(userToTvTable);
                _tvContext.SaveChanges();
            }

            return RedirectToAction(nameof(UserPage));
        }

        [HttpPost]
        public IActionResult UnFollow(int tvMazeId)
        {
            _tvContext.UnFollowDB(tvMazeId, GetUserId());

            return RedirectToAction(nameof(UserPage));
        }


        public int GetUserId()
        {
            // hämta id:t i aspNet
            var aspNetId = _userManager
                .GetUserId(HttpContext.User);

            // hämta id:t i User tabellen
            var userId = _tvContext
                .User
                .FirstOrDefault(i => i.AspNetUserId == aspNetId)
                .Id;
            return userId;
        }

    }
}

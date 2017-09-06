using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using tvShowProject.Models.VM;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tvShowProject.Controllers
{
    public class ShowsController : Controller
    {
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
            return View(userPageVM);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using tvShowProject.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using tvShowProject.Models.VM;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tvShowProject.Controllers
{
    public class AccountsController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        IdentityDbContext _identityContext;
        TvContext _tvContext;

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IdentityDbContext identityContext, TvContext tvContext)
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

        [Authorize] //Lägg överst för att kräva en inloggad session för att komma åt sidorna.
        [HttpGet]
        public IActionResult UserPage()
        {
            UserPageVM userPageVM = new UserPageVM();
            userPageVM.Username = User.Identity.Name;
            return View(userPageVM);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl)
        {
            //var result = await _userManager.CreateAsync(new IdentityUser("user"), "Password123_");

            return View(new AccountLoginVM { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(AccountLoginVM loginVM, UserPageVM userPageVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var result = await _signInManager.PasswordSignInAsync(loginVM.Username, loginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(AccountLoginVM.Username), result.ToString());
                return View(loginVM);
            }

            if (string.IsNullOrWhiteSpace(loginVM.ReturnUrl))
            {
                return RedirectToAction(nameof(UserPage));
            }
            else
                return Redirect(loginVM.ReturnUrl);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            #region Validera vy-modellen
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            #region Skapa användaren
            await _identityContext.Database.EnsureCreatedAsync();
            IdentityUser user = new IdentityUser(model.Username);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Username", result.Errors.First().Description);
                return View(model);
            }
            #endregion

            #region Lägg till i DB
            var userId = await _userManager.GetUserIdAsync(user);
            await _tvContext.AddUser(userId);
            #endregion

            #region Logga in och skicka användaren vidare
            await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            return RedirectToAction(nameof(UserPage));
            #endregion
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

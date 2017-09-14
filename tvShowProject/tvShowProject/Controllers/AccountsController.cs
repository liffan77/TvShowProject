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

        public IActionResult Index()
        {
            return View();
        }

        [Authorize] 
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
                return Redirect("~/Shows/UserPage");
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
            user.Email = model.Email; // sätts just nu till null i DB eftersom vi ej reggar någon mail

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

            return RedirectToAction("UserPage", "Shows");

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost] // ännu ej implementerad
        public IActionResult Settings(string email, string aspNetUserId)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(email))
            {
                return View(new LoggedInUserVM { AspNetId = aspNetUserId, Email = email });
            }

            _identityContext.Users
                .FirstOrDefault(u => u.Id == aspNetUserId)
                .Email = email;

            _identityContext.SaveChanges();

            return RedirectToAction("UserPage", "Shows");
        }

        [HttpGet] // ännu ej implementerad
        public IActionResult Settings()
        {
            var aspNetId = _userManager
                .GetUserId(HttpContext.User);

            var userEmail = _identityContext.Users
                .FirstOrDefault(x => x.Id == aspNetId)
                .Email;

            var userName = _identityContext.Users
                .FirstOrDefault(x => x.Id == aspNetId)
                .UserName;


            LoggedInUserVM loggedInUserVM = new LoggedInUserVM
            {
                AspNetId = aspNetId,
                Username = userName,
                Email = userEmail
            };

            return View(loggedInUserVM);
        }
    }
}

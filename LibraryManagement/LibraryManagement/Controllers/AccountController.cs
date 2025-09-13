using LibraryManagement.ReviewModule;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AccountController(IDataStorageService jsonStorageService) : Controller
    {
        private const string SESSION_USER_KEY = "_User";
        private readonly IDataStorageService _userStorage = jsonStorageService;

        public IActionResult Login()
        {
            ViewBag.Users = _userStorage.GetAllUserNames();
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, bool anonymous = false)
        {
            if (anonymous)
                username = "Гость";

            if (string.IsNullOrWhiteSpace(username))
            {
                ViewBag.Error = "Введите логин ИЛИ просто войдите как 'Гость'";
                ViewBag.Users = _userStorage.GetAllUserNames();
                return View();
            }

            HttpContext.Session.SetString(SESSION_USER_KEY, username);
            _userStorage.AddUser(username);
            return RedirectToAction("Index", "Main");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SESSION_USER_KEY);
            return RedirectToAction("Login");
        }

        public static string? getCurrentUser(HttpContext context) => context.Session.GetString(SESSION_USER_KEY);
    }
}

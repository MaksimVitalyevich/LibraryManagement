using System.Diagnostics;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            var user = AccountController.getCurrentUser(HttpContext);
            if (string.IsNullOrEmpty(user))
                return RedirectToAction("Login", "Account");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

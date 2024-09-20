using CMV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CMV.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View("~/Views/Customer/Login.cshtml"); // Updated path to the Login view
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == "avadheshd@unifycloud.com" && model.Password == "avadheshD43@")
                {
                    // Set session value
                    HttpContext.Session.SetString("Username", model.Username);

                    // Redirect to the default page or another page
                    return RedirectToAction("Index", "Customer");
                }

                // If login fails, show the login page again
                ViewBag.Error = "Invalid login attempt.";
            }
            return View("~/Views/Customer/Login.cshtml", model); // Updated path to the Login view
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Clear();

            // Redirect to the login page
            return RedirectToAction("Login");
        }
    }
}

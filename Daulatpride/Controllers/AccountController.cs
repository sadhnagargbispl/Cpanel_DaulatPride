using Microsoft.AspNetCore.Mvc;
using Daulatpride.Domain.Interface;
using Daulatpride.Domain.Entities;
using Daulatpride.Extension;

namespace Daulatpride.Controllers
{
    public class AccountController : Controller
    {
        private readonly I_Login _loginService;

        public AccountController(I_Login loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login obj)
        {
            if (obj == null)
                return Json(new { errorMessage = "Request data missing." });

            var user = await _loginService.ValidateUser(obj);

            if (user == null)
                return Json(new { errorMessage = "Invalid username or password." });

         
            HttpContext.Session.SetString("UserID", user.IDNo);
            HttpContext.Session.SetString("UserName", user.MemFirstName);
            HttpContext.Session.SetString("Status", "OK");

            HttpContext.Session.SetComplexData("LoginUser", user);

            return Json(new
            {
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

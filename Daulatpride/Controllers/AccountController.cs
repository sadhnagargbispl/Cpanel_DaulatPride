using Microsoft.AspNetCore.Mvc;
using Daulatpride.Domain.Interface;
using Daulatpride.Domain.Entities;
using Daulatpride.Extension;
using DocumentFormat.OpenXml.Spreadsheet;

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
            var member = await _loginService.ValidateUser(obj);
            if (member == null)
            return Json(new { errorMessage = "Invalid username or password." });
            HttpContext.Session.SetInt32("Run", 0);
            HttpContext.Session.SetString("Status", "OK");
            HttpContext.Session.SetString("IDNo", member.IDNo ?? "");
            HttpContext.Session.SetString("MemName", member.MemName ?? "");
            HttpContext.Session.SetString("MobileNo", member.MobileNo ?? "");
            HttpContext.Session.SetInt32("MemKit", member.MemKit);
            HttpContext.Session.SetString("Package", member.Package ?? "");
            HttpContext.Session.SetString("Position", member.Position ?? "");
            HttpContext.Session.SetString("Doj", member.Doj?.ToString("yyyy-MM-dd") ?? "");
            HttpContext.Session.SetString("DOA", member.DOA ?? "");
            HttpContext.Session.SetString("Address", member.Address ?? "");
            HttpContext.Session.SetString("IsFranchise", member.IsFranchise ?? "");
            HttpContext.Session.SetString("ActiveStatus", member.ActiveStatus ?? "");
            HttpContext.Session.SetString("MID", member.MID ?? "");
            HttpContext.Session.SetString("EMail", member.EMail ?? "");
            HttpContext.Session.SetString("ProfilePic", member.ProfilePic ?? "");
            HttpContext.Session.SetString("Panno", member.Panno ?? "");
            HttpContext.Session.SetString("MemPassw", member.MemPassw ?? "");
            HttpContext.Session.SetString("MemEPassw", member.MemEPassw ?? "");
            HttpContext.Session.SetString("Type", member.Type ?? "A");
            HttpContext.Session.SetString("FormNo", member.FormNo.ToString());
            HttpContext.Session.SetString("ActivationDate", member.ActivationDate?.ToString("yyyy-MM-dd") ?? "");
            HttpContext.Session.SetComplexData("LoginUser", member);
            return Json(new { redirectUrl = Url.Action("Index", "Home") });
        }
        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] Login obj)
        //{
        //    if (obj == null)
        //        return Json(new { errorMessage = "Request data missing." });
        //    var member = await _loginService.ValidateUser(obj);
        //    if (member == null)
        //        return Json(new { errorMessage = "Invalid username or password." });
        //    //HttpContext.Session.SetString("UserID", user.IDNo);
        //    //HttpContext.Session.SetString("UserName", user.MemFirstName);
        //    //HttpContext.Session.SetString("Status", "OK");
        //    HttpContext.Session.SetInt32("Run", 0);
        //    HttpContext.Session.SetString("Status", "OK");
        //    HttpContext.Session.SetString("IDNo", member.IDNo ?? "");
        //    HttpContext.Session.SetString("MemName", member.MemName ?? "");
        //    HttpContext.Session.SetString("MobileNo", member.MobileNo ?? "");
        //    HttpContext.Session.SetInt32("MemKit", member.MemKit);
        //    HttpContext.Session.SetString("Package", member.Package ?? "");
        //    HttpContext.Session.SetString("Position", member.Position ?? "");
        //    HttpContext.Session.SetString("Doj", member.Doj?.ToString("yyyy-MM-dd") ?? "");
        //    HttpContext.Session.SetString("DOA", member.DOA ?? "");
        //    HttpContext.Session.SetString("Address", member.Address ?? "");
        //    HttpContext.Session.SetString("IsFranchise", member.IsFranchise ?? "");
        //    HttpContext.Session.SetString("ActiveStatus", member.ActiveStatus ?? "");
        //    HttpContext.Session.SetString("MID", member.MID ?? "");
        //    HttpContext.Session.SetString("EMail", member.EMail ?? "");
        //    HttpContext.Session.SetString("ProfilePic", member.ProfilePic ?? "");
        //    HttpContext.Session.SetString("Panno", member.Panno ?? "");
        //    HttpContext.Session.SetString("MemPassw", member.MemPassw ?? "");
        //    HttpContext.Session.SetString("MemEPassw", member.MemEPassw ?? "");
        //    HttpContext.Session.SetString("Type", member.Type ?? "A");
        //    HttpContext.Session.SetString("FormNo", member.FormNo.ToString());
        //    HttpContext.Session.SetString("MFormno", member.MFormno.ToString());
        //    HttpContext.Session.SetString("MemUpliner", member.MemUpliner.ToString());
        //    HttpContext.Session.SetString("ActivationDate", member.ActivationDate?.ToString("yyyy-MM-dd") ?? "");
        //    HttpContext.Session.SetComplexData("LoginUser", member);
        //    return Json(new { redirectUrl = Url.Action("Index", "Home") });
        //}
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // (Optional) remove cookies if needed
            Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Login");
        }
        //public IActionResult Logout()
        //{
        //    // 🔴 Clear all session data
        //    HttpContext.Session.Clear();
        //    // (Optional) remove cookies if needed
        //    Response.Cookies.Delete(".AspNetCore.Session");
        //    // Redirect to Login page
        //    return RedirectToAction("Login", "Account");
        //}
    }
}

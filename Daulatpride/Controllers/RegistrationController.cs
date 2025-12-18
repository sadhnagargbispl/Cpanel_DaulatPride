using Microsoft.AspNetCore.Mvc;
using Daulatpride.Domain.Interface;
using Daulatpride.Domain.Entities;

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
//using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Text;
using DocumentFormat.OpenXml.Vml;
using System.Security.Cryptography.X509Certificates;
using Daulatpride.Infrastructure.Repository;
//using Daulatpride.Fillters;
namespace Daulatpride.Controllers
{

    public class RegistrationController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly I_Login i_Login_Service;
        private IHttpContextAccessor _httpContextAccessor;
        //private readonly I_Shop i_Shop;
        private readonly I_Report i_report;
        private int CompNo;
        public RegistrationController(ILogger<AccountController> logger, I_Login iLoginService, IHttpContextAccessor httpContextAccessor, I_Report i_report1)
        {
            _logger = logger;
            i_Login_Service = iLoginService;
            _httpContextAccessor = httpContextAccessor;
            //i_Shop = i_Shop1;
            i_report = i_report1;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome(string id)
        {
            try
            {
                string lastId = HttpContext.Session.GetString("LASTID");
                string join = HttpContext.Session.GetString("JOIN");
                string status = HttpContext.Session.GetString("Status");
                string formNo = HttpContext.Session.GetString("Formno");

                string condition = "";

                // 🔹 ASPX: Request["id"]
                if (!string.IsNullOrEmpty(id))
                {
                    if (string.IsNullOrEmpty(lastId) || id != lastId)
                    {
                        TempData["Error"] = "Invalid Access";
                        return RedirectToAction("Logout", "Account");
                    }

                    condition = $" and mMst.IDNo='{id}'";
                }
                else
                {
                    if (join == "YES" && !string.IsNullOrEmpty(lastId))
                    {
                        condition = $" and mMst.IDNo='{lastId}'";
                    }
                    else if (status == "OK" && !string.IsNullOrEmpty(formNo))
                    {
                        condition = $" and mMst.FormNo='{formNo}'";
                    }
                    else
                    {
                        return RedirectToAction("Logout", "Account");
                    }
                }

                // 🔹 DB CALL
                var member = i_report.GetMemberByCondition(condition);

                if (member == null)
                    return RedirectToAction("Logout", "Account");

                var vm = new WelcomeViewModel
                {
                    MemberId = member.IdNo,
                    MemberName = member.MemName,
                    JoiningDate = member.Doj,
                    Password = member.Passw,
                    TransactionPassword = member.EPassw
                };

                // 🔹 ASPX behavior
                if (join == "YES")
                    HttpContext.Session.SetString("JOIN", "FINISH");

                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Logout", "Account");
            }
        }



        public async Task<IActionResult> Registration()
        {
            var userRegistration = new UserRegistration
            {
                CountryTypes = await i_report.GetCountryType(),
                StateTypes = await i_report.GetStateType(),
                Registrationusers = new List<Registration>()
            };

            return View(userRegistration);
        }

        [HttpGet]
        public async Task<IActionResult> GetSponsorName(string sponsorId)
        {
            // 1️⃣ Blank sponsor
            if (string.IsNullOrWhiteSpace(sponsorId))
            {
                return Json(new { success = false, message = "Invalid Sponsor ID" });
            }

            // 2️⃣ DB check
            var sponsorInfo = await i_report.GetSponsorNameAsync(sponsorId);

            // 3️⃣ Not found in DB
            if (sponsorInfo == null)
            {
                return Json(new { success = false, message = "Invalid Sponsor ID" });
            }

            // 4️⃣ Found
            return Json(new
            {
                success = true,
                fullName = sponsorInfo.FullName,
                formNo = sponsorInfo.FormNo,
                kitId = sponsorInfo.KitId
            });
        }


        //[HttpPost]
        //public async Task<IActionResult> Registration([FromBody] Registration model)
        //{
        //    if (model == null)
        //        return Json(new { success = false, message = "No data received" });

        //    if (string.IsNullOrWhiteSpace(model.EmailId))
        //        return Json(new { success = false, message = "Email is required" });

        //    try
        //    {
        //        if (await i_report.IsEmailRegistered(model.EmailId.Trim()))
        //            return Json(new { success = false, message = "Email already registered" });

        //        //if (await i_report.IsMobileRegistered(model.MobileNo))
        //        //    return Json(new { success = false, message = "Mobile already registered" });

        //        await i_report.SaveRegistrationData(model);

        //        return Json(new { success = true, message = "Registration successful" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}[HttpPost]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] Registration model)
        {
            if (model == null)
                return Json(new { success = false, message = "No data received" });

            if (await i_report.IsEmailRegistered(model.EmailId.Trim()))
                         return Json(new { success = false, message = "Email already registered" });

                try
                {
                var profile = await i_report.SaveRegistrationDataAsync(model);

                if (profile == null || string.IsNullOrEmpty(profile.IDNO))
                    return Json(new { success = false, message = "Registration failed" });

                HttpContext.Session.SetString("LASTID", profile.IDNO);
                HttpContext.Session.SetString("JOIN", "YES");

                return Json(new
                {
                    success = true,
                    message = "Registration successful",
                    redirectUrl = Url.Action("Welcome", "Registration", new { id = profile.IDNO })
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}

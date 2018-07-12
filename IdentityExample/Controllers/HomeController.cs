using System.Web.Mvc;
using IdentityExample.ServiceLayer.Contracts;

namespace IdentityExample.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        public HomeController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Silder()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Ads()
        {
            return View();
        }

        public ActionResult Area()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Header()
        {
            return PartialView("~/Views/Shared/_Header.cshtml");
        }
        public ActionResult Footer()
        {
            return PartialView("~/Views/Shared/_Footer.cshtml");
        }
        public ActionResult GetData()
        {
            var applicationUser = _userManager.GetCurrentUser();
            return Content(applicationUser.UserName);
        }
    }
}

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

        public ActionResult GetData()
        {
            var applicationUser = _userManager.GetCurrentUser();
            return Content(applicationUser.UserName);
        }
    }
}

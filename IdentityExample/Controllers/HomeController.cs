using System.Web.Mvc;
using IdentityExample.ServiceLayer.Contracts;
using IdentityExample.ServiceLayer;

namespace IdentityExample.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        readonly FreeContentService _freeContentService;
        readonly SubFreeContentService _subFreeContentService;

        public HomeController(IApplicationUserManager userManager, FreeContentService freeContentService, SubFreeContentService subFreeContentService)
        {
            _userManager = userManager;
            _freeContentService = freeContentService;
            _subFreeContentService = subFreeContentService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Silder()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("Silder");
            return PartialView("_Silder", model);
        }

        public ActionResult Services()
        {
            var model = _freeContentService.GeFreeContentByType("Services");
            return PartialView("_Services", model);
        }

        public ActionResult Ads()
        {
            var model = _freeContentService.GeFreeContentByType("Ads");

            return PartialView("_Ads", model);
        }

        public ActionResult Area()
        {
            var model = _freeContentService.GeFreeContentByType("Area");

            return PartialView("_Area", model);
        }

        [Authorize]
        public ActionResult About()
        {
            var model = _freeContentService.GeFreeContentByType("About");
            ViewBag.Message = "Your app description page.";

            return PartialView("_About", model);
        }

        public ActionResult Contact()
        {
            var model = _freeContentService.GeFreeContentByType("Contact");

            ViewBag.Message = "Your contact page.";
            return PartialView("_Contact", model);
        }

        public ActionResult Header()
        {
            var model = _freeContentService.GeFreeContentByType("Header");

            return PartialView("_Header", model);
        }
        public ActionResult Footer()
        {
            var model = _freeContentService.GeFreeContentByType("Footer");
            return PartialView("_Footer", model);
        }
        public ActionResult GetData()
        {
            var applicationUser = _userManager.GetCurrentUser();
            return Content(applicationUser.UserName);
        }
    }
}

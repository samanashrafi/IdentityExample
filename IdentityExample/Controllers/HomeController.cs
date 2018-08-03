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
            var model = _subFreeContentService.GetSubFreeContentByType("Services");
            return PartialView("_Services", model);
        }

        public ActionResult Ads()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("Ads");

            return PartialView("_Ads", model);
        }

        public ActionResult Area()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("Area");
            return PartialView("_Area", model);
        }

        public ActionResult News()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("News");
            return PartialView("_News", model);
        }
        public ActionResult Menu()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("Menu");

            return PartialView("_Menu", model);
        }
        [Authorize]
        public ActionResult About()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("About");
            ViewBag.Message = "Your app description page.";

            return PartialView("_About", model);
        }

        public ActionResult Contact()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("Contact");

            ViewBag.Message = "Your contact page.";
            return PartialView("_Contact", model);
        }

        public ActionResult Details(int id)
        {
            var model = _subFreeContentService.GetSubByFreeContentId(id);
            return View(model);
        }

        
        public ActionResult GetData()
        {
            var applicationUser = _userManager.GetCurrentUser();
            return Content(applicationUser.UserName);
        }
    }
}

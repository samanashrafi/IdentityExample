using System.Web.Mvc;
using IdentityExample.ServiceLayer.Contracts;
using IdentityExample.ServiceLayer;
using IdentityExample.DomainClasses;
using IdentityExample.DataLayer;

namespace IdentityExample.Controllers
{

    public class HomeController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        readonly FreeContentService _freeContentService;
        readonly SubFreeContentService _subFreeContentService;
        readonly ContactService _contactServiceService;
        readonly IUnitOfWork _uow;

        public HomeController(IApplicationUserManager userManager, FreeContentService freeContentService, SubFreeContentService subFreeContentService
            , ContactService contactService, IUnitOfWork uow)
        {
            _userManager = userManager;
            _freeContentService = freeContentService;
            _subFreeContentService = subFreeContentService;
            _contactServiceService = contactService;
            _uow = uow;
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

        [HttpPost]
        public JsonResult CreateMessage(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactServiceService.Create(contact);
                _uow.SaveAllChanges();
                var tResult = new { Success = "True", Message = "آیتم با موفقیت درج گردید" };
                return Json(tResult, JsonRequestBehavior.AllowGet);
            }


            var result = new { Success = "flase", Message = "حطا در ثبت اطلاعات" };

            return Json(result, JsonRequestBehavior.AllowGet);


        }
        public ActionResult Tariffs()
        {
            return View();
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

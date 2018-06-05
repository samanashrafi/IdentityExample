//using System.Web.Mvc;
//using IdentityExample.DataLayer;
//using IdentityExample.DomainClasses;
//using IdentityExample.ServiceLayer.Contracts;
//using IdentityExample.ServiceLayer;

//namespace AspNetIdentityDependencyInjectionSample.Controllers
//{
//    [Authorize]
//    public class SubFreeContentController : Controller
//    {
//        readonly IFreeContentService _freeContentService;
//        readonly IUnitOfWork _uow;
//        public SubFreeContentController(IUnitOfWork uow, IFreeContentService freeContentService)
//        {
//            _freeContentService = freeContentService;
//            _uow = uow;
//        }

//        [HttpGet]
//        public ActionResult Index()
//        {
//            var list = _freeContentService.GetAll();
//            return View(list);
//        }

//        [HttpGet]
//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Create(FreeContent freeContent)
//        {
//            if (this.ModelState.IsValid)
//            {
//                _freeContentService.Create(freeContent);
//                _uow.SaveAllChanges();
//            }
//            return RedirectToAction("Index");
//        }

//        [HttpGet]
//        public ActionResult Edit(int id)
//        {
//            var model = _freeContentService.GetById(id);
//            return View(model);
//        }

//        [HttpPost]
//        public ActionResult Edit(FreeContent freeContent)
//        {
//            if (this.ModelState.IsValid)
//            {
//                _freeContentService.Update(freeContent);
//                _uow.SaveAllChanges();
//            }
//            return RedirectToAction("Index");
//        }

//        public ActionResult Delete(FreeContent freeContent)
//        {
//            _freeContentService.Delete(freeContent);

//            _uow.SaveAllChanges();

//            return RedirectToAction("Index");
//        }
//    }
//}
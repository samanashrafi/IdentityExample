using System.Web.Mvc;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer.Contracts;
using IdentityExample.ServiceLayer;

namespace IdentityExample.Controllers
{
    [Authorize]
    public class FreeContentController : Controller
    {
        readonly IFreeContentService _freeContentService;
        readonly IUnitOfWork _uow;
        public FreeContentController(IUnitOfWork uow, IFreeContentService freeContentService)
        {
            _freeContentService = freeContentService;
            _uow = uow;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var list = _freeContentService.GetAll();
            return View(list);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(FreeContent freeContent)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        _freeContentService.Create(freeContent);
        //        _uow.SaveAllChanges();
        //    }

        //    return RedirectToAction("Index");
        //    //return Json(freeContent, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult Data()
        //{
        //    var model = _freeContentService.GetAll();
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

         

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _freeContentService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(FreeContent freeContent)
        {
            if (this.ModelState.IsValid)
            {
                _freeContentService.Update(freeContent);
                _uow.SaveAllChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _freeContentService.Delete(id);

            _uow.SaveAllChanges();

            return RedirectToAction("Index");
        }
    }
}
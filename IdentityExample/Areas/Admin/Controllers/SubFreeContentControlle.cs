using System.Web.Mvc;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System;

namespace IdentityExample.Areas.Admin.Controllers
{
    [Authorize]
    public class SubFreeContentController : Controller
    {
        readonly SubFreeContentService _subFreeContentService;
        readonly FreeContentService _freeContentService;
        readonly IUnitOfWork _uow;
        //readonly ImageService _image;
        public SubFreeContentController(IUnitOfWork uow, SubFreeContentService subFreeContentService,
            FreeContentService freeContentService)
        {
            _subFreeContentService = subFreeContentService;
            _freeContentService = freeContentService;
            _uow = uow;
        }

        [HttpGet]

        public ActionResult Index(int id)

        {
            var model = _freeContentService.GetById(id);
            ViewBag.SubFreeContent = _subFreeContentService.GetSubByFreeContentId(id);
            return View(model);
        }

        [HttpGet]
        public JsonResult Data(RequestData model, int id)
        {
            var list = _subFreeContentService.GetSubFreeContentByFreeId(id);
            var searchModel = _subFreeContentService.GetSubFreeContentListBySearch(model.searchPhrase);
            int skipRows = (model.current - 1) * model.rowCount;
            if (searchModel != null)
            {
                //return Json(searchModel, JsonRequestBehavior.AllowGet);
                var f = new ResponseData<List<SubFreeContent>>()
                {
                    current = model.current,
                    rowCount = model.rowCount,
                    rows = searchModel.Skip(skipRows).Take(model.rowCount).ToList(),
                    total = searchModel.Count
                };
                return Json(f, JsonRequestBehavior.AllowGet);
            }

            var tResult = new ResponseData<List<SubFreeContent>>()
            {
                current = model.current,
                rowCount = model.rowCount,
                rows = list.Skip(skipRows).Take(model.rowCount).ToList(),
                total = list.Count
            };
            return Json(tResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        //[ValidateAjax]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(SubFreeContent subFreeContent, HttpPostedFileBase file)
        {

            var files = Request.Files;
            if (ModelState.IsValid)
            {
             
                subFreeContent.Image = new Image(); // for check not null

                if (file != null && file.ContentLength > 0)
                {
                    
                    var path = Server.MapPath("~") + "upload\\" + file.FileName;
                    subFreeContent.Image.Name = file.FileName;
                    subFreeContent.Image.Url = file.FileName;

                    file.SaveAs(path);
                }

                _subFreeContentService.Create(subFreeContent);
                _uow.SaveAllChanges();
                var tResult = new { Success = "True", Message = "آیتم با موفقیت درج گردید" };
                return Json(tResult, JsonRequestBehavior.AllowGet);
            }


            var result = new { Success = "flase", Message = "حطا در ثبت اطلاعات" };

            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        // [ChildActionOnly]
        public ActionResult Edit(int id)
        {
            var model = _subFreeContentService.GetById(id);
            ViewBag.SubFreeContent = model;
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(SubFreeContent subFreeContent, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {
                _subFreeContentService.Update(subFreeContent);
                _uow.SaveAllChanges();
                //var tResult = SubFreeContent;
                return Json(new { success = true, responseText = "ویرایش زیر محتوای آزاد با موفقت ثبت شد." }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, responseText = "ویرایش زیر محتوای آزاد با خطا مواج شد." }, JsonRequestBehavior.AllowGet);

            }


            //return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _subFreeContentService.Delete(id);

            _uow.SaveAllChanges();
            //var tResult = _subFreeContentService.GetById(id);
            return Json("حذف آیتم انجام شد.", JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult MultiDelete(List<int> id)
        {
            if (id != null && id.Count > 0)
            {
                foreach (var item in id)
                {
                    _subFreeContentService.Delete(item);
                }
                _uow.SaveAllChanges();
                return Json("آیتم‌های مورد نظر با موفقیت حذف شدند.");
            }

            return Json("عملیات حدف با حطا مواجه شده است ");
        }

        [HttpPost]
        public JsonResult Search(string term)
        {
            var model = _subFreeContentService.GetSubFreeContentListBySearch(term);
            if (model != null)
                return Json(model);
            return Json("نتیجه ای یافت نشد", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubFreeContentById(int id)
        {
            var model = _subFreeContentService.GetSubFreeContentById(id);
            return PartialView("DashboardItems", model);
        }

        
    }
}
using System.Web.Mvc;
using Newtonsoft.Json;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer.Contracts;
using IdentityExample.ServiceLayer;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System;
using System.Net;
using IdentityExample.Filters;
using System.Threading.Tasks;
using System.Web;

namespace IdentityExample.Areas.Admin.Controllers
{
    [Authorize]
    public class FreeContentController : Controller
    {
        readonly FreeContentService _freeContentService;
        readonly SubFreeContentService _subFreeContentService;
        readonly IUnitOfWork _uow;
        public FreeContentController(IUnitOfWork uow, FreeContentService freeContentService, SubFreeContentService subFreeContentService)
        {
            _freeContentService = freeContentService;
            _subFreeContentService = subFreeContentService;
            _uow = uow;
        }

        [HttpGet]

        public ActionResult Index()

        {

            //var list = _freeContentService.GetAll();

            return View();
        }

        [HttpGet]
        public JsonResult Data(RequestData model)
        {
            var list = _freeContentService.GetAllJson();
            var searchModel = _freeContentService.GetFreeContentListBySearch(model.searchPhrase);
            int skipRows = (model.current - 1) * model.rowCount;
            if (searchModel != null)
            {
                //return Json(searchModel, JsonRequestBehavior.AllowGet);
                var f = new ResponseData<List<FreeContent>>()
                {
                    current = model.current,
                    rowCount = model.rowCount,
                    rows = searchModel.Skip(skipRows).Take(model.rowCount).ToList(),
                    total = searchModel.Count
                };
                return Json(f, JsonRequestBehavior.AllowGet);
            }

            var tResult = new ResponseData<List<FreeContent>>()
            {
                current = model.current,
                rowCount = model.rowCount,
                rows = list.Skip(skipRows).Take(model.rowCount).ToList(),
                total = list.Count
            };
            return Json(tResult, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[ValidateAjax]
        [HttpPost]

        public JsonResult Create(FreeContent freeContent/*, HttpPostedFileBase uploadImage*/)
        {
            if (ModelState.IsValid)
            {
                //string path = Server.MapPath("~") + "Admin\\FreeContentImage\\" + uploadImage.FileName;

                //string path = Server.MapPath("~") + "upload\\" + uploadImage.FileName;
                //if (uploadImage != null)
                //{
                //    Images image = new Images
                //    {
                //        Name = uploadImage.FileName,
                //        Url = path,
                //        FreeContent= freeContent,
                //        FreeContentId =1
                //    };

                //    uploadImage.SaveAs(path);
                //    //freeContent.Images.Add(image);
                //    _image.Create(image);
                //    _uow.SaveAllChanges();
                //}
                _freeContentService.Create(freeContent);
                _uow.SaveAllChanges();
                var tResult = new { Success = "True", Message = "آیتم با موفقیت درج گردید" };
                return Json(tResult, JsonRequestBehavior.AllowGet);
            }


            var result = new { Success = "flase", Message = "حطا در ثبت اطلاعات" };

            return Json(result, JsonRequestBehavior.AllowGet);


        }

        // [HttpGet]
        //// [ChildActionOnly]
        // public ActionResult Edit(int id)
        // {
        //     var model = _freeContentService.GetById(id);
        //     ViewBag.FreeContent = model;
        //     return PartialView("_Edit", model);
        // }

        [HttpPost]
        public JsonResult Edit(FreeContent freeContent)
        {


            if (ModelState.IsValid)
            {
                _freeContentService.Update(freeContent);
                _uow.SaveAllChanges();
                //var tResult = freeContent;
                return Json(new { success = true, responseText = "ویرایش محتوای آزاد با موفقت ثبت شد." }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, responseText = "ویرایش محتوای آزاد با خطا مواج شد." }, JsonRequestBehavior.AllowGet);

            }


            //return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _freeContentService.Delete(id);

            _uow.SaveAllChanges();
            //var tResult = _freeContentService.GetById(id);
            return Json("حذف آیتم انجام شد.", JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult MultiDelete(List<int> Id)
        {
            if (Id != null && Id.Count > 0)
            {
                foreach (var item in Id)
                {
                    _freeContentService.Delete(item);
                }
                _uow.SaveAllChanges();
                return Json("آیتم‌های مورد نظر با موفقیت حذف شدند.");
            }

            return Json("عملیات حدف با حطا مواجه شده است ");
        }

        [HttpPost]
        public JsonResult Search(string term)
        {
            var model = _freeContentService.GetFreeContentListBySearch(term);
            if (model != null)
                return Json(model);
            return Json("نتیجه ای یافت نشد", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DashboardItems()
        {
            var model = _freeContentService.GetAll();
            return PartialView("DashboardItems", model);
        }


    }
}
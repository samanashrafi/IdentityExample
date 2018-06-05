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
    public class SubFreeContentController : Controller
    {
        readonly SubFreeContentService _SubFreeContentService;
        readonly FreeContentService _freeContentService;
        readonly IUnitOfWork _uow;
        //readonly ImageService _image;
        public SubFreeContentController(IUnitOfWork uow, SubFreeContentService SubFreeContentService,
            FreeContentService freeContentService)
        {
            _SubFreeContentService = SubFreeContentService;
            _freeContentService = freeContentService;
            _uow = uow;
        }

        [HttpGet]

        public ActionResult Index(int id)

        {
            var model = _freeContentService.GetById(id);
            ViewBag.SubFreeContent = _SubFreeContentService.GetSubByFreeContentId(id);//_freeContentService.GetSubByFreeContentId(id);
            return View(model);
        }

        [HttpGet]
        public JsonResult Data(RequestData model, int id)
        {
            var list = _SubFreeContentService.GetSubFreeContentByFreeId(id);
            var searchModel = _SubFreeContentService.GetSubFreeContentListBySearch(model.searchPhrase);
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

        public JsonResult Create(SubFreeContent SubFreeContent/*, HttpPostedFileBase uploadImage*/)
        {
            if (ModelState.IsValid)
            {
                //string path = Server.MapPath("~") + "Admin\\SubFreeContentImage\\" + uploadImage.FileName;

                //string path = Server.MapPath("~") + "upload\\" + uploadImage.FileName;
                //if (uploadImage != null)
                //{
                //    Images image = new Images
                //    {
                //        Name = uploadImage.FileName,
                //        Url = path,
                //        SubFreeContent = SubFreeContent,
                //        SubFreeContentId = 1
                //    };

                //    uploadImage.SaveAs(path);
                //    //SubFreeContent.Images.Add(image);
                //    _image.Create(image);
                //    _uow.SaveAllChanges();
                //}
                _SubFreeContentService.Create(SubFreeContent);
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
        //     var model = _SubFreeContentService.GetById(id);
        //     ViewBag.SubFreeContent = model;
        //     return PartialView("_Edit", model);
        // }

        [HttpPost]
        public JsonResult Edit(SubFreeContent SubFreeContent)
        {


            if (ModelState.IsValid)
            {
                _SubFreeContentService.Update(SubFreeContent);
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
            _SubFreeContentService.Delete(id);

            _uow.SaveAllChanges();
            //var tResult = _SubFreeContentService.GetById(id);
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
                    _SubFreeContentService.Delete(item);
                }
                _uow.SaveAllChanges();
                return Json("آیتم‌های مورد نظر با موفقیت حذف شدند.");
            }

            return Json("عملیات حدف با حطا مواجه شده است ");
        }

        [HttpPost]
        public JsonResult Search(string term)
        {
            var model = _SubFreeContentService.GetSubFreeContentListBySearch(term);
            if (model != null)
                return Json(model);
            return Json("نتیجه ای یافت نشد", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubFreeContentById(int id)
        {
            var model = _SubFreeContentService.GetSubFreeContentById(id);
            return PartialView("DashboardItems", model);
        }
    }
}
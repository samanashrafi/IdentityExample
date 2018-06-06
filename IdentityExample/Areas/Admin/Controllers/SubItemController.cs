using System.Web.Mvc;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer;
using System.Linq;
using System.Collections.Generic;
using System;


namespace IdentityExample.Areas.Admin.Controllers
{
    [Authorize]
    public class SubItemController : Controller
    {
        readonly SubItemService _subItemService;
        readonly SubFreeContentService _subFreeContentService;
        readonly IUnitOfWork _uow;
        //readonly ImageService _image;
        public SubItemController(IUnitOfWork uow, SubItemService subItemService,
            SubFreeContentService subFreeContentService)
        {
            _subItemService = subItemService;
            _subFreeContentService = subFreeContentService;
            _uow = uow;
        }

        [HttpGet]

        public ActionResult Index(int id)

        {
            var model = _subFreeContentService.GetById(id);
            ViewBag.SubItem = _subItemService.GetSubItemBySubFreeContentId(id);
            return View(model);
        }
        
        [HttpGet]
        public JsonResult Data(RequestData model, int id)
        {
            var list = _subItemService.GetSubItemListBySubFreeContentId(id);
            var searchModel = _subItemService.GetSubItemListBySearch(model.searchPhrase);
            int skipRows = (model.current - 1) * model.rowCount;
            if (searchModel != null)
            {
                //return Json(searchModel, JsonRequestBehavior.AllowGet);
                var f = new ResponseData<List<SubItem>>()
                {
                    current = model.current,
                    rowCount = model.rowCount,
                    rows = searchModel.Skip(skipRows).Take(model.rowCount).ToList(),
                    total = searchModel.Count
                };
                return Json(f, JsonRequestBehavior.AllowGet);
            }

            var tResult = new ResponseData<List<SubItem>>()
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

        public JsonResult Create(SubItem subItem/*, HttpPostedFileBase uploadImage*/)
        {
            if (ModelState.IsValid)
            {
                _subItemService.Create(subItem);
                _uow.SaveAllChanges();
                var tResult = new { Success = "True", Message = "آیتم با موفقیت درج گردید" };
                return Json(tResult, JsonRequestBehavior.AllowGet);
            }


            var result = new { Success = "flase", Message = "حطا در ثبت اطلاعات" };

            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult Edit(SubItem SubItem)
        {


            if (ModelState.IsValid)
            {
                _subItemService.Update(SubItem);
                _uow.SaveAllChanges();
                //var tResult = SubItem;
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
            _subItemService.Delete(id);
            _uow.SaveAllChanges();
            return Json("حذف آیتم انجام شد.", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult MultiDelete(List<int> Id)
        {
            if (Id != null && Id.Count > 0)
            {
                foreach (var item in Id)
                {
                    _subItemService.Delete(item);
                }
                _uow.SaveAllChanges();
                return Json("آیتم‌های مورد نظر با موفقیت حذف شدند.");
            }

            return Json("عملیات حدف با حطا مواجه شده است ");
        }

        [HttpPost]
        public JsonResult Search(string term)
        {
            var model = _subItemService.GetSubItemListBySearch(term);
            if (model != null)
                return Json(model);
            return Json("نتیجه ای یافت نشد", JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetSubItemById(int id)
        //{
        //    var model = _subItemService.GetSubItemBySubFreeContentId(id);
        //    return PartialView("DashboardItems", model);
        //}
    }
}
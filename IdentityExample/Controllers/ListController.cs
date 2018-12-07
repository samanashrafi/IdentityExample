using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityExample.ServiceLayer.Contracts;
using IdentityExample.ServiceLayer;
using IdentityExample.DataLayer;

namespace IdentityExample.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        readonly FreeContentService _freeContentService;
        readonly SubFreeContentService _subFreeContentService;
        readonly PageMetaDetailService _pageMetaDetailService;
        readonly IUnitOfWork _uow;

        public ListController(FreeContentService freeContentService, SubFreeContentService subFreeContentService, 
            IUnitOfWork uow, PageMetaDetailService pageMetaDetailService)
        {
            _freeContentService = freeContentService;
            _subFreeContentService = subFreeContentService;
            _uow = uow;
            _pageMetaDetailService = pageMetaDetailService;
        }
        public ActionResult AreaList()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("Area");
            return View(model);
        }
        public ActionResult NewsList()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("News");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var model = _subFreeContentService.GetById(id);
            return View(model);
        }
        public ActionResult OtherList(int id)
        {
            var model = _subFreeContentService.GetSubFreeContentByType("News");
            return View(model);
        }

    }
}
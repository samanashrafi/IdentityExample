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
    public class AboutMeController : Controller
    {
        // GET: AboutMe
        

        readonly FreeContentService _freeContentService;
        readonly SubFreeContentService _subFreeContentService;
        readonly IUnitOfWork _uow;
        readonly PageMetaDetailService _pageMetaDetailService;

        public AboutMeController(FreeContentService freeContentService, SubFreeContentService subFreeContentService, IUnitOfWork uow,
             PageMetaDetailService pageMetaDetailService)
        {
            _freeContentService = freeContentService;
            _subFreeContentService = subFreeContentService;
            _pageMetaDetailService = pageMetaDetailService;
            _uow = uow;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Services()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("Services");
            return View(model);
        }

    }
}
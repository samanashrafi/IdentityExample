using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityExample.ServiceLayer.Contracts;
using IdentityExample.ServiceLayer;

namespace IdentityExample.Controllers
{
    public class AboutMeController : Controller
    {
        // GET: AboutMe
        public ActionResult Index()
        {
            return View();
        }

        readonly FreeContentService _freeContentService;
        readonly SubFreeContentService _subFreeContentService;

        public AboutMeController(FreeContentService freeContentService, SubFreeContentService subFreeContentService)
        {
            _freeContentService = freeContentService;
            _subFreeContentService = subFreeContentService;
        }
        public ActionResult Services()
        {
            var model = _subFreeContentService.GetSubFreeContentByType("Services");
            return View(model);
        }

    }
}
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityExample.Areas.Admin.Controllers
{
    public class TestController : Controller
    {

        // GET: Admin/Test
        readonly FreeContentService _freeContentService;
        readonly IUnitOfWork _uow;
        public TestController(IUnitOfWork uow, FreeContentService freeContentService)
        {
            _freeContentService = freeContentService;
            _uow = uow;
        }
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Create(FreeContent model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _freeContentService.Create(model);
        //        _uow.SaveAllChanges();
        //    }
        //}
    }
}
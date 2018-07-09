using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityExample.Controllers
{
    public class MyHeaderController : Controller
    {
        // GET: MyHeader
        public ActionResult Header()
        {
            return PartialView("~/Views/Shared/_Header.cshtml");
        }
    }
}
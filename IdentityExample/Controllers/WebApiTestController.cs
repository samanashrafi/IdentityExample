using System.Web.Mvc;

namespace IdentityExample.Controllers
{
    [Authorize]
    public class WebApiTestController : Controller
    {
        // GET: WebApiTest
        public ActionResult Index()
        {
            return View();
        }
    }
}
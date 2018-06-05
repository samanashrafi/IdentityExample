using System.Web.Mvc;

namespace IdentityExample.Controllers
{
    [Authorize]
    public class SignalRTestController : Controller
    {
        // GET: SignalRTest
        public ActionResult Index()
        {
            return View();
        }
    }
}
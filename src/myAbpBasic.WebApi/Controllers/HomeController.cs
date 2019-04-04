using Microsoft.AspNetCore.Mvc;

namespace myAbpBasic.Web.Controllers
{
    public class HomeController : myAbpBasicControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

   
    }
}
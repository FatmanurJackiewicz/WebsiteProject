using Microsoft.AspNetCore.Mvc;

namespace AdminPanelMVC.Controllers
{
    public class PagesAdminController : Controller
    {
        [Route("/pages")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

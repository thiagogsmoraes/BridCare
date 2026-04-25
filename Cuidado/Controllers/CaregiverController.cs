using Microsoft.AspNetCore.Mvc;

namespace Cuidado.Controllers
{
    public class CaregiverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

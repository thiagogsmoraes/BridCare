using Microsoft.AspNetCore.Mvc;

namespace Cuidado.Controllers
{
    public class InstitutionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

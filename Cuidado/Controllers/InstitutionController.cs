using Cuidado.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cuidado.Controllers
{
    public class InstitutionController : Controller
    {
        private readonly InstitutionService _service;

        public InstitutionController(InstitutionService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int id)
        {
            var obj = await _service.FindByUserIdAsync(id);
            return View(obj);
        }
    }
}

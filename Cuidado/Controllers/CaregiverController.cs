using Cuidado.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cuidado.Controllers
{
    public class CaregiverController : Controller
    {
        private readonly CaregiverService _caregiverService;

        public CaregiverController(CaregiverService caregiverService)
        {
            _caregiverService = caregiverService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var obj = await _caregiverService.FindByIdAsync(id);
            return View(obj);
        }
    }
}

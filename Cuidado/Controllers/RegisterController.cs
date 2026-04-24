using Cuidado.Models.ViewModels;
using Cuidado.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cuidado.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _registerService.RegisterAsync(vm);
            return RedirectToAction(nameof(Create));
        }
    }
}

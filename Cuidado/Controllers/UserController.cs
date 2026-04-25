using Cuidado.Models;
using Cuidado.Models.ViewModels;
using Cuidado.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cuidado.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
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
        public async Task<IActionResult> Create(UserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _userService.RegisterAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Login(string email)
        {
            var obj = await _userService.FindByEmailAsync(email);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var routeValues = new { id = obj.Id };

            if (obj.Role == 0)
            {
                return RedirectToAction("Index", "Caregiver", routeValues);
            }
            return RedirectToAction("Index", "Institution", routeValues);
        }
    }
}

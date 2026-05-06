using Cuidado.Models;
using Cuidado.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cuidado.Controllers
{
    [Authorize(Policy = "OnlyCaregiver")]
    public class CaregiverController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CaregiverService _caregiverService;

        public CaregiverController(UserManager<User> userManager, CaregiverService caregiverService)
        {
            _userManager = userManager;
            _caregiverService = caregiverService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User); // pega o Id do logado
            var caregiver = await _caregiverService.FindByUserIdAsync(userId);
            return View(caregiver);
        }
    }
}

using BridCare.Models;
using BridCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BridCare.Controllers
{
    [Authorize(Policy = "OnlyInstitution")]
    public class InstitutionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly InstitutionService _service;

        public InstitutionController(UserManager<User> userManager, InstitutionService service)
        {
            _userManager = userManager;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var institution = await _service.FindByUserIdAsync(userId);
            return View(institution);
        }
    }
}

using BridCare.Models;
using BridCare.Models.Enums;
using BridCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BridCare.Controllers
{
    [Authorize(Policy = "OnlyInstitution")]
    public class InstitutionApplicationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly InstitutionApplicationService _institutionApplicationService;

        public InstitutionApplicationController(UserManager<User> userManager, InstitutionApplicationService institutionApplicationService)
        {
            _userManager = userManager;
            _institutionApplicationService = institutionApplicationService;
        }

        public async Task<IActionResult> Index(int id)
        {
            string userId = _userManager.GetUserId(User);
            var applications = await _institutionApplicationService.FindAllApplicationsAsync(userId, id);

            ViewData["id"] = id;

            return View(applications);
        }

        public async Task<IActionResult> UpdateStatus(int id, bool accepted, int shiftId)
        {
            if (accepted)
            {
                await _institutionApplicationService.UpdateApplicationStatusAsync(id, ApplicationStatus.Accepted);
            }
            else
            {
                await _institutionApplicationService.UpdateApplicationStatusAsync(id, ApplicationStatus.Rejected);
            }

            return RedirectToAction(nameof(Index), new { id = shiftId });
        }
    }
}

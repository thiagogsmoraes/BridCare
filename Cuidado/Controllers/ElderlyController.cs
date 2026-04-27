using AspNetCoreGeneratedDocument;
using Cuidado.Models;
using Cuidado.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Cuidado.Controllers
{
    [Authorize(Policy = "OnlyInstitution")]
    public class ElderlyController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ElderlyService _elderlyService;
        private readonly InstitutionService _institutionService;

        public ElderlyController(UserManager<User> userManager, ElderlyService service, InstitutionService institution)
        {
            _userManager = userManager;
            _elderlyService = service;
            _institutionService = institution;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);

            ViewBag.InstitutionName = institution.Name;

            var elderlies = await _elderlyService.FindAllAsync(userId);
            return View(elderlies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Elderly elderly)
        {
            if (!ModelState.IsValid)
            {
                return View(elderly);
            }

            var userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);
            elderly.InstitutionId = institution.Id;

            await _elderlyService.AddElderlyAsync(elderly);
            return RedirectToAction(nameof(Index));
        }
    }
}

using AspNetCoreGeneratedDocument;
using BridCare.Models;
using BridCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BridCare.Controllers
{
    [Authorize(Policy = "OnlyInstitution")]
    public class ElderlyController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ElderlyService _elderlyService;
        private readonly InstitutionService _institutionService;
        private readonly ShiftService _shiftService;

        public ElderlyController(UserManager<User> userManager, ElderlyService service, InstitutionService institution, ShiftService shiftService)
        {
            _userManager = userManager;
            _elderlyService = service;
            _institutionService = institution;
            _shiftService = shiftService;
        }

        public async Task<IActionResult> Index(string name)
        {
            var userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);

            ViewData["Institution"] = institution.Name;
            ViewData["Elderly"] = name;

            List<Elderly> elderlies;      

            if (string.IsNullOrEmpty(name))
            {
                elderlies = await _elderlyService.FindAllAsync(userId);
            }
            else
            {
                elderlies = await _elderlyService.FindByNameAsync(userId, name);
            }

            ViewData["Count"] = await _institutionService.CountAllElderliesAsync(userId);
            return View(elderlies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var elderly = await _elderlyService.FindByUserIdAsync(userId, id);
            return View(elderly);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var elderly = await _elderlyService.FindByUserIdAsync(userId, id);
            return View(elderly);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Elderly elderly)
        {
            if (!ModelState.IsValid)
            {
                return View(elderly);
            }

            var userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);

            elderly.InstitutionId = institution.Id;
            
            await _elderlyService.UpdateElderlyAsync(elderly);
            return RedirectToAction(nameof(Details), new { id = elderly.Id});
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
            await _shiftService.UpdateCountElderliesAsync(institution.Id, await _institutionService.CountAllElderliesAsync(userId));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var elderly = await _elderlyService.FindByUserIdAsync(userId, id);
            return View(elderly);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);
            var elderly = await _elderlyService.FindByUserIdAsync(userId, id);

            await _elderlyService.DeleteElderlyAsync(id);
            await _shiftService.UpdateCountElderliesAsync(institution.Id, await _institutionService.CountAllElderliesAsync(userId));
            return RedirectToAction(nameof(Index));
        }
    }
}

using AspNetCoreGeneratedDocument;
using Cuidado.Models;
using Cuidado.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Cuidado.Controllers
{
    public class ElderlyController : Controller
    {
        private readonly ElderlyService _service;
        private readonly InstitutionService _institution;

        public ElderlyController(ElderlyService service, InstitutionService institution)
        {
            _service = service;
            _institution = institution;
        }

        public async Task<IActionResult> Index(int id)
        {
            var institution = await _institution.FindByInstitutionIdAsync(id);

            ViewBag.InstitutionId = id;
            ViewBag.UserId = institution.UserId;

            var elderlies = await _service.FindAllAsync(id);
            return View(elderlies);
        }

        public IActionResult Create(int id)
        {
            ViewBag.InstitutionId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Elderly elderly)
        {
            Console.WriteLine($"InstitutionId recebido: {elderly.InstitutionId}");
            if (!ModelState.IsValid)
            {
                return View(elderly);
            }

            var routeValues = new { id = elderly.InstitutionId};

            await _service.AddElderlyAsync(elderly);
            return RedirectToAction(nameof(Index), routeValues);
        }
    }
}

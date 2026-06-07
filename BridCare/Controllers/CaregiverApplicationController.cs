using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BridCare.Data;
using BridCare.Models;
using BridCare.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BridCare.Controllers
{
    [Authorize(Policy = "OnlyCaregiver")]
    public class CaregiverApplicationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly CaregiverApplicationService _service;
        private readonly ShiftService _shiftService;
        private readonly CaregiverService _caregiverService;

        public CaregiverApplicationController(AppDbContext context, UserManager<User> userManager, CaregiverApplicationService service, ShiftService shiftService, CaregiverService caregiverService)
        {
            _context = context;
            _userManager = userManager;
            _service = service;
            _shiftService = shiftService;
            _caregiverService = caregiverService;
        }

        // GET: Application
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);
            var applications = await _service.FindAllAplicationsAsync(userId);
            return View(applications);
        }

        public async Task<IActionResult> Shifts()
        {
            string userId = _userManager.GetUserId(User);
            // busca turnos abertos disponíveis
            var shifts = await _shiftService.FindAllOpenShiftsAsync();
            return View(shifts);
        }

        // GET: Shift/Details/5
        public async Task<IActionResult> ShiftDetails(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _shiftService.FindByIdAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(int id)
        {
            string userId = _userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            }

            var application = await _service.FindByUserIdAsync(userId, id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(int id)
        {
            string userId = _userManager.GetUserId(User);
            var shift = await _shiftService.FindByIdAsync(id);
            var caregiver = await _caregiverService.FindByUserIdAsync(userId);

            var application = new Application
            {
                ShiftId = shift.Id,
                CaregiverId = caregiver.Id
            };

            await _service.AddApplicationAsync(application);
            return RedirectToAction(nameof(Index));
        }

        // GET: Application/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["CaregiverId"] = new SelectList(_context.Caregivers, "Id", "Id", application.CaregiverId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Id", application.ShiftId);
            return View(application);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShiftId,CaregiverId,Status,Message,AppliedAt")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaregiverId"] = new SelectList(_context.Caregivers, "Id", "Id", application.CaregiverId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Id", application.ShiftId);
            return View(application);
        }

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            string userId = _userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            }

            var application = await _service.FindByUserIdAsync(userId, id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = _userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            }

            await _service.DeleteApplicationAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cuidado.Data;
using Cuidado.Models;
using Cuidado.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;
using AspNetCoreGeneratedDocument;

namespace Cuidado.Controllers
{
    [Authorize(Policy = "OnlyInstitution")]
    public class ShiftController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly InstitutionService _institutionService;
        private readonly ShiftService _shiftService;

        public ShiftController(UserManager<User> userManager, InstitutionService institutionService, ShiftService shiftService)
        {
            _userManager = userManager;
            _institutionService = institutionService;
            _shiftService = shiftService;
        }

        // GET: Shift
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);
            List<Shift> shifts = await _shiftService.FindAllAsync(userId);
            return View(shifts);
        }

        // GET: Shift/Details/5
        public async Task<IActionResult> Details(int id)
        {
            string userId = _userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            };

            var shift = await _shiftService.FindByUserIdAsync(userId, id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shift/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shift/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return View(shift);
            }

            string userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);

            shift.InstitutionId = institution.Id;
            shift.ElderlyQuantity = await _institutionService.CountAllElderliesAsync(userId);

            await _shiftService.AddShiftAsync(shift);
            return RedirectToAction(nameof(Index));
        }

        // GET: Shift/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            string userId = _userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            }

            var shift = await _shiftService.FindByUserIdAsync(userId, id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shift/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shift shift)
        {
            string userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);

            if (id != shift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    shift.InstitutionId = institution.Id;
                    shift.ElderlyQuantity = await _institutionService.CountAllElderliesAsync(userId);
                    await _shiftService.UpdateShiftAsync(shift);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = shift.Id });
            }

            return View(shift);
        }

        // GET: Shift/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            string userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);

            if (id == null)
            {
                return NotFound();
            }

            var shift = await _shiftService.FindByUserIdAsync(userId, id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = _userManager.GetUserId(User);
            var institution = await _institutionService.FindByUserIdAsync(userId);

            if (id == null)
            {
                return NotFound();
            }

            await _shiftService.DeleteShiftAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(int id)
        {
            //return _context.Shifts.Any(e => e.Id == id);
            return false;
        }
    }
}

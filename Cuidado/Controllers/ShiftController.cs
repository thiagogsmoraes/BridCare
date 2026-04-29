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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            };

            //var shift = await _context.Shifts
            //.Include(s => s.Caregiver)
            //.Include(s => s.Institution)
            //.FirstOrDefaultAsync(m => m.Id == id);
            //if (shift == null)
            //{
            //return NotFound();
            //}

            //return View(shift);
            return View();
        }

        // GET: Shift/Create
        public IActionResult Create()
        {
            // ViewData["CaregiverId"] = new SelectList(_context.Caregivers, "Id", "Id");
            //ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Id");
            return View();
        }

        // POST: Shift/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var shift = await _context.Shifts.FindAsync(id);
            //if (shift == null)
            // {
            //     return NotFound();
            // }
            // ViewData["CaregiverId"] = new SelectList(_context.Caregivers, "Id", "Id", shift.CaregiverId);
            // ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Id", shift.InstitutionId);
            // return View(shift);
            return View();
        }

        // POST: Shift/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InstitutionId,StartTime,EndTime,Price,ElderlyQuantity,NursingKnowledgeRequired,CaregiversPerShift,Description,Status,CaregiverId,CreatedAt")] Shift shift)
        {
            if (id != shift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(shift);
                    //await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            // ViewData["CaregiverId"] = new SelectList(_context.Caregivers, "Id", "Id", shift.CaregiverId);
            //ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Id", shift.InstitutionId);
            return View(shift);
        }

        // GET: Shift/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var shift = await _context.Shifts
            //  .Include(s => s.Caregiver)
            //  .Include(s => s.Institution)
            //  .FirstOrDefaultAsync(m => m.Id == id);
            // if (shift == null)
            // {
            //    return NotFound();
            //}

            //return View(shift);
            return View();
        }

        // POST: Shift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var shift = await _context.Shifts.FindAsync(id);
            //if (shift != null)
            //{
            //_context.Shifts.Remove(shift);
            //}

            // await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return View();
        }

        private bool ShiftExists(int id)
        {
            //return _context.Shifts.Any(e => e.Id == id);
            return false;
        }
    }
}

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

namespace Cuidado.Controllers
{
    public class CaregiverApplicationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly CaregiverApplicationService _service;

        public CaregiverApplicationController(AppDbContext context, UserManager<User> userManager, CaregiverApplicationService service)
        {
            _context = context;
            _userManager = userManager;
            _service = service;
        }

        // GET: Application
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Applications.Include(a => a.Caregiver).Include(a => a.Shift);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Caregiver)
                .Include(a => a.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Application/Create
        public IActionResult Create()
        {
            ViewData["CaregiverId"] = new SelectList(_context.Caregivers, "Id", "Id");
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Id");
            return View();
        }

        // POST: Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShiftId,CaregiverId,Status,Message,AppliedAt")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaregiverId"] = new SelectList(_context.Caregivers, "Id", "Id", application.CaregiverId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Id", application.ShiftId);
            return View(application);
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Caregiver)
                .Include(a => a.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Shifts()
        {
            string userId = _userManager.GetUserId(User);
            // busca turnos abertos disponíveis
            var shifts = await _service.FindAllAsync();
            return View(shifts);
        }
    }
}

using Cuidado.Data;
using Cuidado.Models;
using Microsoft.EntityFrameworkCore;

namespace Cuidado.Services
{
    public class ShiftService
    {
        private readonly AppDbContext _context;

        public ShiftService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shift>> FindAllAsync(string id)
        {
            return await _context.Shifts.Include(x => x.Institution).Where(x => x.Institution.UserId == id).OrderBy(x => x.StartTime).ToListAsync();
        }

        public async Task<List<Shift>> FindAllOpenShiftsAsync()
        {
            return await _context.Shifts.Include(x => x.Institution).OrderBy(x => x.StartTime).ToListAsync();
        }

        public async Task<Shift> FindByIdAsync(int id)
        {
            return await _context.Shifts.Include(x => x.Institution).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Shift> FindByUserIdAsync(string id, int shiftId)
        {
            return await _context.Shifts.Include(x => x.Institution).FirstOrDefaultAsync(x => x.Id == shiftId && x.Institution.UserId == id);
        }

        public async Task AddShiftAsync(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShiftAsync(Shift shift)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCountElderliesAsync(int institutionId, int countElderlies)
        {
            await _context.Shifts.Include(x => x.Institution).Where(x => x.Institution.Id == institutionId).ExecuteUpdateAsync(x => x.SetProperty(x => x.ElderlyQuantity, countElderlies));
        }

        public async Task DeleteShiftAsync(int id)
        {
            var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.Id == id);
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
        }
    }
}

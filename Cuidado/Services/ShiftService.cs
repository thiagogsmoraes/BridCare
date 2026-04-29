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
        
        public async Task AddShiftAsync(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
        }
    }
}

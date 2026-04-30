using Cuidado.Data;
using Cuidado.Models;
using Microsoft.EntityFrameworkCore;

namespace Cuidado.Services
{
    public class CaregiverApplicationService
    {
        private readonly AppDbContext _context;

        public CaregiverApplicationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shift>> FindAllAsync()
        {
            return await _context.Shifts.Include(x => x.Institution).OrderBy(x => x.StartTime).ToListAsync();
        }
    }
}

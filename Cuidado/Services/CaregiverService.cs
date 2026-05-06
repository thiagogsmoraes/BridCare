using Cuidado.Data;
using Cuidado.Models;
using Microsoft.EntityFrameworkCore;

namespace Cuidado.Services
{
    public class CaregiverService
    {
        private readonly AppDbContext _context;

        public CaregiverService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Caregiver> FindByUserIdAsync(string id)
        {
            return await _context.Caregivers.FirstOrDefaultAsync(x => x.UserId == id);
        }
    }
}

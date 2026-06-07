using BridCare.Data;
using BridCare.Models;
using Microsoft.EntityFrameworkCore;

namespace BridCare.Services
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

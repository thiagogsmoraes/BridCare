using BridCare.Data;
using BridCare.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BridCare.Services
{
    public class InstitutionApplicationService
    {
        private readonly AppDbContext _context;

        public InstitutionApplicationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<int, int>> CountAllApplicationsAsync(string id)
        {
            return await _context.Applications.Include(x => x.Shift).ThenInclude(x => x.Institution).Where(x => x.Shift.Institution.UserId == id).GroupBy(x => x.ShiftId).ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<List<Application>> FindAllApplicationsAsync(string userId, int id)
        {
            return await _context.Applications.Include(x => x.Shift).ThenInclude(x => x.Institution).Include(x => x.Caregiver).Where(x => x.Shift.Institution.UserId == userId && x.ShiftId == id).ToListAsync();
        }
    }
}

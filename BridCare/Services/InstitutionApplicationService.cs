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

        public async Task<Dictionary<int, int>> CountAllApplications(string id)
        {
            return await _context.Applications.Include(x => x.Shift).ThenInclude(x => x.Institution).Where(x => x.Shift.Institution.UserId == id).GroupBy(x => x.ShiftId).ToDictionaryAsync(g => g.Key, g => g.Count());
        }
    }
}

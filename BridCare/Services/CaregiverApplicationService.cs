using BridCare.Data;
using BridCare.Models;
using Microsoft.EntityFrameworkCore;

namespace BridCare.Services
{
    public class CaregiverApplicationService
    {
        private readonly AppDbContext _context;

        public CaregiverApplicationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Application>> FindAllAplicationsAsync(string id)
        {
            return await _context.Applications.Include(x => x.Shift).ThenInclude(x => x.Institution).Include(x => x.Caregiver).Where(x => x.Caregiver.UserId == id).OrderBy(x => x.Shift.StartTime).ToListAsync();
        }

        public async Task<Application> FindByUserIdAsync(string id, int applicationId)
        {
            return await _context.Applications.Include(x => x.Shift).ThenInclude(x => x.Institution).Include(x => x.Caregiver).FirstOrDefaultAsync(x => x.Id == applicationId && x.Caregiver.UserId == id);
        }

        public async Task AddApplicationAsync(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteApplicationAsync(int id)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(x => x.Id == id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
        }
    }
}

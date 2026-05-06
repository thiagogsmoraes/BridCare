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

        public async Task<List<Application>> FindAllAplicationsAsync(string id)
        {
            return await _context.Applications.Include(x => x.Shift).ThenInclude(x => x.Institution).Include(x => x.Caregiver).Where(x => x.Caregiver.UserId == id).ToListAsync();
        }

        public async Task<Application> FindByUserIdAsync(string id)
        {
            return await _context.Applications.Include(x => x.Shift).ThenInclude(x => x.Institution).Include(x => x.Caregiver).FirstOrDefaultAsync(x => x.Caregiver.UserId == id);
        }

        public async Task AddApplicationAsync(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
        }
    }
}

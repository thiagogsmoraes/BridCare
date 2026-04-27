using Cuidado.Data;
using Cuidado.Models;
using Microsoft.EntityFrameworkCore;

namespace Cuidado.Services
{
    public class ElderlyService
    {
        private readonly AppDbContext _context;

        public ElderlyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Elderly>> FindAllAsync(string id)
        {
            var institutionId = await _context.Institutions.FirstOrDefaultAsync(x => x.UserId == id);
            return await _context.Elderlies.Include(x => x.Institution).Where(x => x.InstitutionId == institutionId.Id).ToListAsync();
        }

        public async Task AddElderlyAsync(Elderly elderly)
        {
            _context.Elderlies.Add(elderly);
            await _context.SaveChangesAsync();
        }
    }
}

using BridCare.Data;
using BridCare.Models;
using Microsoft.EntityFrameworkCore;

namespace BridCare.Services
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
            return await _context.Elderlies.Include(x => x.Institution).Where(x => x.Institution.UserId == id).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<List<Elderly>> FindByNameAsync(string id, string elderlyName)
        {
            return await _context.Elderlies.Include(x => x.Institution).Where(x => x.Name.Contains(elderlyName) && x.Institution.UserId == id).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Elderly> FindByUserIdAsync(string id, int elderlyId)
        {
            return await _context.Elderlies.Include(x => x.Institution).FirstOrDefaultAsync(x => x.Id == elderlyId && x.Institution.UserId == id);
        }

        public async Task AddElderlyAsync(Elderly elderly)
        {
            _context.Elderlies.Add(elderly);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateElderlyAsync(Elderly elderly)
        {
            _context.Elderlies.Update(elderly);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteElderlyAsync(int id)
        {
            var elderly = await _context.Elderlies.FirstOrDefaultAsync(x => x.Id == id);
            _context.Elderlies.Remove(elderly);
            await _context.SaveChangesAsync();
        }
    }
}

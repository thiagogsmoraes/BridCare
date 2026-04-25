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

        public async Task<List<Elderly>> FindAllAsync(int id)
        {
            return await _context.Elderlies.Where(x => x.InstitutionId == id).ToListAsync();
        }

        public async Task AddElderlyAsync(Elderly m)
        {
            var elderly = new Elderly
            {
                InstitutionId = m.InstitutionId,
                Name = m.Name,
                BirthDate = m.BirthDate,
                Gender = m.Gender,
                Condition = m.Condition,
                Notes = m.Notes,
                CreatedAt = DateTime.Now
            };

            _context.Elderlies.Add(elderly);
            await _context.SaveChangesAsync();
        }
    }
}

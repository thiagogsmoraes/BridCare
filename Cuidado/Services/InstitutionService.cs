using Cuidado.Data;
using Cuidado.Models;
using Microsoft.EntityFrameworkCore;

namespace Cuidado.Services
{
    public class InstitutionService
    {
        private readonly AppDbContext _context;

        public InstitutionService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<Institution> FindByUserIdAsync(int id)
        {
            return await _context.Institutions.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Institution> FindByInstitutionIdAsync(int id)
        {
            return await _context.Institutions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

using Cuidado.Data;
using Cuidado.Models;
using Cuidado.Models.Enums;
using Cuidado.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Cuidado.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context) 
        {
            _context = context;
        }
        
        // Creating New User and New Caregiver or Institution
        public async Task RegisterAsync(UserViewModel vm)
        {
            var user = new User { Email = vm.Email, Role = vm.Role, CreatedAt = DateTime.Now};
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (vm.Role == Role.Caregiver)
            {
                var caregiver = new Caregiver
                {
                    UserId = user.Id,
                    FullName = vm.FullName!,
                    CPF = vm.CPF!,
                    BirthDate = vm.BirthDate!.Value,
                    Gender = vm.Gender!.Value,
                    Phone = vm.Phone!,
                    Email = vm.Email,
                    Adress = vm.Adress!,
                    Description = vm.Description,
                    EducationLevel = vm.EducationLevel!.Value,
                    ExpectedSalary = vm.ExpectedSalary!.Value,
                    CreatedAt = DateTime.Now,
                };
                _context.Add(caregiver);
            }

            if (vm.Role == Role.Instituition)
            {
                var institution = new Institution
                {
                    UserId = user.Id,
                    Name = vm.Name!,
                    Cnpj = vm.Cnpj!,
                    Phone = vm.InstitutionPhone!,
                    Whatsapp = vm.Whatsapp!,
                    Email = vm.InstitutionEmail!,
                    Adress = vm.InstitutionAdress!,
                    Type = vm.InstitutionType!.Value,
                    Description = vm.InstitutionDescription!,
                    CreatedAt = DateTime.Now,
                };
                _context.Add(institution);
            }

            await _context.SaveChangesAsync();
        }

        // Finding Email
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

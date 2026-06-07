using System.Security.Claims;
using BridCare.Data;
using BridCare.Models;
using BridCare.Models.Enums;
using BridCare.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BridCare.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager) 
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Entering in Account
        public async Task LoginAsync(LoginViewModel model)
        {
            // Checagem de email e senha no banco é feita de forma automática pelo Identity
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception("Email ou senha inválidos");
            }
        }

        // Finding Email
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }


        // Creating New User and New Caregiver or Institution
        public async Task RegisterAsync(UserViewModel vm)
        {
            // Criando usuário conforme Identity e inputando no banco automaticamente - ele cria um hash para o password
            var user = new User { Email = vm.Email, UserName = vm.Email, Role = vm.Role};
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded) 
            {
                throw new Exception(result.Errors.First().Description);
            }

            // Só é necessário criar o Claim de Role pq os outros o Identity (Id, Email, Username) já cria internamente
            await _userManager.AddClaimAsync(user, new Claim("Role", vm.Role.ToString()));

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
                _context.Caregivers.Add(caregiver);
            }

            if (vm.Role == Role.Institution)
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
                _context.Institutions.Add(institution);
            }

            await _context.SaveChangesAsync();
        }
    }
}

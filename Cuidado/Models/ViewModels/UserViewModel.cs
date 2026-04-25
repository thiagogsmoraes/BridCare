using System.ComponentModel.DataAnnotations;
using Cuidado.Models.Enums;

namespace Cuidado.Models.ViewModels
{
    public class UserViewModel
    {
        [Required] [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Role Role { get; set; }


        // Data of Caregiver
        public string? FullName { get; set; }
        public string? CPF { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Adress { get; set; }
        public string? Description { get; set; }
        public EducationLevel? EducationLevel { get; set; }
        public double? ExpectedSalary { get; set; }

        // Data of Institution
        public string? Name { get; set; }
        public string? Cnpj { get; set; }
        public string? InstitutionPhone { get; set; }
        public string? Whatsapp { get; set; }
        public string? InstitutionEmail { get; set; }
        public string? InstitutionAdress { get; set; }
        public Category? InstitutionType { get; set; }
        public string? InstitutionDescription { get; set; }
    }
}

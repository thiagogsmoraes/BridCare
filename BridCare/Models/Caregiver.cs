using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BridCare.Models.Enums;

namespace BridCare.Models
{
    public class Caregiver
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        [DisplayName("Nome")]
        public string FullName { get; set; }
        public string CPF { get; set; }
        [DisplayName("Data de Nascimento")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Gênero")]
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Endereço")]
        public string Adress { get; set; }
        [DisplayName("Descrição")]
        public string? Description { get; set; }
        [DisplayName("Escolaridade")]
        public EducationLevel EducationLevel { get; set; }
        [DisplayName("Pagamento Esperado")] [DisplayFormat(DataFormatString = "{0:F2}")]
        public double ExpectedSalary { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Application> Applications { get; set; } = new List<Application>();

        public Caregiver() { }

        public Caregiver(User user, string fullName, string cpf, DateTime birthDate, Gender gender, string phone, string email, string adress, string description, EducationLevel educationLevel, double expectedSalary, DateTime createdAt)
        {
            User = user;
            FullName = fullName;
            CPF = cpf;
            BirthDate = birthDate;
            Gender = gender;
            Phone = phone;
            Email = email;
            Adress = adress;
            Description = description;
            EducationLevel = educationLevel;
            ExpectedSalary = expectedSalary;
            CreatedAt = createdAt;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Caregiver
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string FullName { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string? Description { get; set; }
        public EducationLevel EducationLevel { get; set; }
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


        //Adding and Removing from Application List
        public void AddApplication(Application application)
        {
            Applications.Add(application);
        }

        public void RemoveApplication(Application application)
        {
            Applications.Remove(application);
        }
    }
}

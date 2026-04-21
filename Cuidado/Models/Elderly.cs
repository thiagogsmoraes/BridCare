using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Elderly
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }
        public Institutions Institution { get; set; }

        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public Condition Condition { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        public Elderly() { }

        public Elderly(int id, Institutions Institution, DateTime birthDate, Gender gender, Condition condition, string notes, DateTime createdAt)
        {
            Id = id;
            Institution = Institution;
            BirthDate = birthDate;
            Gender = gender;
            Condition = condition;
            Notes = notes;
            CreatedAt = createdAt;
        }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Elderly
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }
        public Institution? Institution { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Data de Nascimento")] [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Gênero")]
        public Gender Gender { get; set; }
        [DisplayName("Condição")]
        public Condition Condition { get; set; }
        [DisplayName("Notas")]
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        public Elderly() { }

        public Elderly(Institution institution, string name, DateTime birthDate, Gender gender, Condition condition, string notes, DateTime createdAt)
        {
            Institution = institution;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            Condition = condition;
            Notes = notes;
            CreatedAt = createdAt;
        }
    }
}

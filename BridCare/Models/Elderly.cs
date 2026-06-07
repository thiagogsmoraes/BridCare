using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BridCare.Models.Enums;

namespace BridCare.Models
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
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

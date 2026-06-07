using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BridCare.Models.Enums;

namespace BridCare.Models
{
    public class Shift
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }
        public Institution? Institution { get; set; }

        [Required][DisplayName("Início")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime StartTime { get; set; }
        [Required][DisplayName("Término")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime EndTime { get; set; }
        [Required][DisplayName("Preço")][DisplayFormat(DataFormatString = "{0:F2}")]
        public double Price { get; set; }
        [DisplayName("Quantidade de Idosos")]
        public int ElderlyQuantity { get; set; }
        [Required][DisplayName("Experiência")]
        public bool NursingKnowledgeRequired { get; set; }
        [Required][DisplayName("Cuidadores por Turno")]
        public int CaregiversPerShift { get; set; }
        [DisplayName("Descrição")]
        public string? Description { get; set; }
        public ShiftStatus Status { get; set; } = ShiftStatus.Open;

        public int? CaregiverId { get; set; }
        [DisplayName("Cuidador")]
        public Caregiver? Caregiver { get; set; }

        [DisplayName("Criado em")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}

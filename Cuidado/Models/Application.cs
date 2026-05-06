using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Application
    {
        public int Id { get; set; }

        public int ShiftId { get; set; }
        [DisplayName("Plantão")]
        public Shift? Shift { get; set; }

        public int CaregiverId { get; set; }
        public Caregiver? Caregiver { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        [DisplayName("Mensagem")]
        public string? Message { get; set; }
        [DisplayName("Aplicado em")] [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime AppliedAt { get; set; } = DateTime.Now;
    }
}

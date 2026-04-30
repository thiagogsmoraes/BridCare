using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Application
    {
        public int Id { get; set; }

        public int ShiftId { get; set; }
        public Shift Shift { get; set; }

        public int CaregiverId { get; set; }
        public Caregiver Caregiver { get; set; }

        public ApplicationStatus Status { get; set; }
        public string Message { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}

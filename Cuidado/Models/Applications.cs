using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Applications
    {
        public int Id { get; set; }

        public int ShiftId { get; set; }
        public Shifts Shift { get; set; }

        public int CaregiverId { get; set; }
        public Caregivers Caregiver { get; set; }

        public ApplicationStatus Status { get; set; }
        public string Message { get; set; }
        public DateTime AppliedAt { get; set; }

        public Applications() { }

        public Applications(int id, Shifts shift, Caregivers caregiver, ApplicationStatus status, string message, DateTime appliedAt)
        {
            Id = id;
            Shift = shift;
            Caregiver = caregiver;
            Status = status;
            Message = message;
            AppliedAt = appliedAt;
        }
    }
}

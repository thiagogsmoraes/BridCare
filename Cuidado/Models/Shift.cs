using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Shift
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Price { get; set; }
        public int ElderlyQuantity { get; set; }
        public bool NursingKnowledgeRequired { get; set; }
        public int CaregiversPerShift { get; set; }
        public string? Description { get; set; }
        public ShiftStatus Status { get; set; }

        public int CaregiverId { get; set; }
        public Caregiver? Caregiver { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Application> Applications { get; set; } = new List<Application>();

        public Shift() { }

        public Shift(Institution institution, DateTime startTime, DateTime endTime, double price, int elderlyQuantity, bool nursingKnowledgeRequired, int caregiversPerShift, string description, ShiftStatus status, Caregiver caregiver, DateTime createdAt)
        {
            Institution = institution;
            StartTime = startTime;
            EndTime = endTime;
            Price = price;
            ElderlyQuantity = elderlyQuantity;
            NursingKnowledgeRequired = nursingKnowledgeRequired;
            CaregiversPerShift = caregiversPerShift;
            Description = description;
            Status = status;
            Caregiver = caregiver;
            CreatedAt = createdAt;
        }
    }
}

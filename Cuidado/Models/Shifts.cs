using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Shifts
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }
        public Institutions Institution { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Price { get; set; }
        public int ElderlyQuantity { get; set; }
        public bool NursingKnowledgeRequired { get; set; }
        public int CaregiversPerShift { get; set; }
        public string? Description { get; set; }
        public ShiftStatus Status { get; set; }

        public int CaregiverId { get; set; }
        public Caregivers? Caregiver { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Applications> Applications { get; set; } = new List<Applications>();

        public Shifts() { }

        public Shifts(int id, Institutions Institution, DateTime startTime, DateTime endTime, double price, int elderlyQuantity, bool nursingKnowledgeRequired, int caregiversPerShift, string description, ShiftStatus status, Caregivers caregiver, DateTime createdAt)
        {
            Id = id;
            Institution = Institution;
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


        //Adding and Removing from Application List
        public void AddApplication(Applications application)
        {
            Applications.Add(application);
        }

        public void RemoveApplication(Applications application)
        {
            Applications.Remove(application);
        }
    }
}

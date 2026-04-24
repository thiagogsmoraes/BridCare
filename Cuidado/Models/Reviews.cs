using System.ComponentModel.DataAnnotations.Schema;

namespace Cuidado.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        public int FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        [InverseProperty("ReviewsGiven")]
        public Users FromUser { get; set; }

        public int ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        [InverseProperty("ReviewsReceived")]
        public Users ToUser { get; set; }

        public int ShiftId { get; set; }
        public Shifts Shift { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public Reviews() { }

        public Reviews(Users fromUser, Users toUser, int shiftId, Shifts shift, int rating, string comment, DateTime createdAt)
        {
            FromUser = fromUser;
            ToUser = toUser;
            ShiftId = shiftId;
            Shift = shift;
            Rating = rating;
            Comment = comment;
            CreatedAt = createdAt;
        }
    }
}

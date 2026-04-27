using System.ComponentModel.DataAnnotations.Schema;

namespace Cuidado.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        [InverseProperty("ReviewsGiven")]
        public User FromUser { get; set; }

        public string ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        [InverseProperty("ReviewsReceived")]
        public User ToUser { get; set; }

        public int ShiftId { get; set; }
        public Shift Shift { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public Review() { }

        public Review(User fromUser, User toUser, int shiftId, Shift shift, int rating, string comment, DateTime createdAt)
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

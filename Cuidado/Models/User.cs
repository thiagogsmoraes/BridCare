using System.ComponentModel.DataAnnotations.Schema;
using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }

        [InverseProperty("FromUser")]
        public ICollection<Review> ReviewsGiven { get; set; } = new List<Review>();

        [InverseProperty("ToUser")]
        public ICollection<Review> ReviewsReceived { get; set; } = new List<Review>();

        public User() { }

        public User(string email, Role role, DateTime createdAt)
        {
            Email = email;
            Role = role;
            CreatedAt = createdAt;
        }


        // Adding and Removing from Reviews Given List
        public void AddReviewGiven(Review review)
        {
            ReviewsGiven.Add(review);
        }

        public void RemoveReviewGiven(Review review)
        {
            ReviewsGiven.Remove(review);
        }

        // Adding and Removing from Reviews Received List
        public void AddReviewReceived(Review review)
        {
            ReviewsReceived.Add(review);
        }

        public void RemoveReviewReceived(Review review)
        {
            ReviewsReceived.Remove(review);
        }
    }
}

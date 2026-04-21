using System.ComponentModel.DataAnnotations.Schema;
using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }

        [InverseProperty("FromUser")]
        public ICollection<Reviews> ReviewsGiven { get; set; } = new List<Reviews>();

        [InverseProperty("ToUser")]
        public ICollection<Reviews> ReviewsReceived { get; set; } = new List<Reviews>();

        public Users() { }

        public Users(int id, string email, Role role, DateTime createdAt)
        {
            Id = id;
            Email = email;
            Role = role;
            CreatedAt = createdAt;
        }


        // Adding and Removing from Reviews Given List
        public void AddReviewGiven(Reviews review)
        {
            ReviewsGiven.Add(review);
        }

        public void RemoveReviewGiven(Reviews review)
        {
            ReviewsGiven.Remove(review);
        }

        // Adding and Removing from Reviews Received List
        public void AddReviewReceived(Reviews review)
        {
            ReviewsReceived.Add(review);
        }

        public void RemoveReviewReceived(Reviews review)
        {
            ReviewsReceived.Remove(review);
        }
    }
}

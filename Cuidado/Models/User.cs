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
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Cuidado.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Cuidado.Models
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; } = true;
        public Role Role { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [InverseProperty("FromUser")]
        public ICollection<Review> ReviewsGiven { get; set; } = new List<Review>();

        [InverseProperty("ToUser")]
        public ICollection<Review> ReviewsReceived { get; set; } = new List<Review>();
    }
}

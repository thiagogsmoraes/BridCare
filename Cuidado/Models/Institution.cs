using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Institution
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Phone { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        [DisplayName("Categoria")]
        public Category Type { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Elderly> Elderlies { get; set; } = new List<Elderly>();
        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();

        public Institution() { }

        public Institution(User user, string name, string cnpj, string phone, string whatsapp, string email, string adress, Category type, string description, DateTime createdAt)
        {
            User = user;
            Name = name;
            Cnpj = cnpj;
            Phone = phone;
            Whatsapp = whatsapp;
            Email = email;
            Adress = adress;
            Type = type;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Cuidado.Models.Enums;

namespace Cuidado.Models
{
    public class Institutions
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Phone { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public Category Type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Elderly> Elderlies { get; set; } = new List<Elderly>();
        public ICollection<Shifts> Shifts { get; set; } = new List<Shifts>();

        public Institutions() { }

        public Institutions(int id, Users user, string name, string cnpj, string phone, string whatsapp, string email, string adress, Category type, string description, DateTime createdAt)
        {
            Id = id;
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


        // Addind and Removing from Elderlies List
        public void AddElderly(Elderly elderly)
        {
            Elderlies.Add(elderly);
        }

        public void RemoveElderly(Elderly elderly)
        {
            Elderlies.Remove(elderly);
        }

        public int CountOfElderlies()
        {
            return Elderlies.Count();
        }


        //Adding and Removing from Shifts List
        public void AddShift(Shifts shift)
        {
            Shifts.Add(shift);
        }

        public void RemoveShift(Shifts shift)
        {
            Shifts.Remove(shift);
        }
    }
}

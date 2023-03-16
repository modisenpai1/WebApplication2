using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Data;

namespace WebApplication2.Domain.Models
{
    public class Orginization //IEntity
    {
        [Key]
        public int Id { get; set; }
        public string? FacebookLink { get; set; }
        public string? TwitterLink{ get; set; }

        public string? InstagramLink { get; set; }
        public string? ImgPath { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; } 
        public ICollection<Event> Events { get; set; }
        public ICollection<Adress> Adresses  { get; set; }
       public ICollection<UserOrg> UserOrgs { get; set; }

        public Orginization()
        {
            
            Events = new List<Event>();
            Adresses= new List<Adress>();
            UserOrgs= new List<UserOrg>();

            

        }




    }
}

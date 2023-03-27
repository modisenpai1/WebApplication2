using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Data;

namespace WebApplication2.Domain.Models
{
    public class Country//IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<User> UsersInCountry { get; set; }
        public ICollection<Adress> Adresses { get; set; }
        public Country()
        {
            Cities= new List<City>();
            UsersInCountry= new List<User>();
            Adresses= new List<Adress>();
            Events= new List<Event>();
            
        }


    }
}

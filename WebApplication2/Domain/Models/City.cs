using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Data;
using WebApplication2.Domain.DTOs;

namespace WebApplication2.Domain.Models
{
    public class City //: IEntity
    {
      
        public City()
        {
           
            UserInCity = new List<User>();
            eventsAtCity= new List<Event>();

        }
       
        public int Id { get; set; }
        public string CityName { get; set; }
    
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]

        public Country country { get; set; } 
        public ICollection<User> UserInCity { get; set; }
    
        public ICollection<Event> eventsAtCity { get; set; }

       


    }

}

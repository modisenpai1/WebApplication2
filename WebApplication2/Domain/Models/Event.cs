using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Data;

namespace WebApplication2.Domain.Models
{
    public class Event //IEntity
    {
        
  
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public Accessibality accessibality { get; set; }
        public bool IsOnline { get; set; }
    
        public string Link { get; set; }
        public bool hasMaxCap { get; set; }
        public int MaxCap { get; set; }
        public int? AdressId{ get; set; }
        public ICollection<EventUser> EventUsers { get; set; }
        [ForeignKey("AdressId")]
        public Adress Adress { get; set; }
        public int OrginizationId { get; set; }
        [ForeignKey("OrginizationId")]
        public Orginization Orginization { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public Event()
        {
           
        EventUsers = new List<EventUser>();

        }
    

    }
    public enum Accessibality
    {
        Public = 0
    , Private = 1
    , inviteOnly = 2

    }

}

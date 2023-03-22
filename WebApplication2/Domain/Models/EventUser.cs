using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Domain.Models
{
    [PrimaryKey("UserId", "EventId")]
    public class EventUser
    {
  
        public string UserId { get; set; }
       
        public User User { get; set; }
     
        public int EventId { get; set; }
        public Event Event { get; set; }
        public DateTime NotifyMe { get; set; }
        public EventRole role { get; set; }
            }                                                                       
    public enum EventRole { 
            EventCreator=1,
            EventParticpant=0  
    }
}
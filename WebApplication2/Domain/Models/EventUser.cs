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
        public DateTime? NotifyMe { get; set; }
        public EventRole Role { get; set; }
            }                                                                       
    public enum EventRole { 
            Creator,
            Administrator,
            Particpant
    }
}
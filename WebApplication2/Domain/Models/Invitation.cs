using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Domain.Models
{
    
    public class Invitation
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public Status Status { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsSeen { get; set; } = false;
    }
    public enum Status
    {
        Pending,
        Accepted,
        Declined,
        Expired
    }
}

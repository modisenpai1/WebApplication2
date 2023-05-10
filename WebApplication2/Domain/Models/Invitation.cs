using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Domain.Models
{
    
    public class Invitation
    {
        public int Id { get; set; }
        public string InvitedUserId { get; set; }
        [ForeignKey("InvitedUserId")]
        public User InvitedUser { get; set; }

        public string InvitingUserId { get; set; }
        [ForeignKey("InvitingUserId")]
        public User InvitingUser { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }

        public Status Status { get; set; } = 0;
        public DateTime ExpirationDate { get; set; }
        public bool IsSeen { get; set; } = false;
    }
    public enum Status
    {
        Pending,
        Accepted,
        Declined,
        Expired,
        Cancled
    }
}

using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class UserEventReadDto
    {
        public int EventId { get; set; }
        public EventRefDto Event { get; set; }
        public DateTime NotifyMe { get; set; }
        public EventRole role { get; set; }
    }
    public class EventUserReadDto 
    {
        public UserRefDto User { get; set; }
        public DateTime NotifyMe { get; set; }
        public EventRole role { get; set; }
    }
    public class EventUserCreateDto
    {
        public string UserId { get; set; }
        public int EventId { get; set; }
        public DateTime NotifyMe { get; set; }
        public EventRole role { get; set; }
    }
}

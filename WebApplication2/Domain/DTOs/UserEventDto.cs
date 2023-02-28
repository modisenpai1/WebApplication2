using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class UserEventReadDto
    {
        public int EventId { get; set; }
        public EventReadDto Event { get; set; }
        public DateTime NotifyMe { get; set; }
        public Role role { get; set; }
    }
    public class EventUserReadDto 
    {
        public string UserId { get; set; }
        public UserReadDto User { get; set; }
        public DateTime NotifyMe { get; set; }
        public Role role { get; set; }
    }
    public class EventUserCreateDto
    {
        public string UserId { get; set; }
        public UserReadDto User { get; set; }
        public int EventId { get; set; }
        public EventReadDto Event { get; set; }
        public DateTime NotifyMe { get; set; }
        public Role role { get; set; }
    }
}

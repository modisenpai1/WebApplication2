using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class UserEventDto
    {
        public int EventId { get; set; }
        public EventReadDto Event { get; set; }
        public DateTime NotifyMe { get; set; }
        public Role role { get; set; }
    }
    public class EventUserDto 
    {
        public int EventId { get; set; }
        public EventReadDto Event { get; set; }
        public DateTime NotifyMe { get; set; }
        public Role role { get; set; }
    }

}

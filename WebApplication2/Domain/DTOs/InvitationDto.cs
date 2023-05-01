using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class InvitationCreateDto
    {
        public string UserId { get; set; }
        public int EventId { get; set; }
    }
    public class InvitationReadDto 
    {
        public UserRefDto UserRef { get; set; }
        public EventRefDto EventRef { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Status Status { get; set; }
    }
}

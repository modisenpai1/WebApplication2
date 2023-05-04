using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class InvitationCreateDto
    {
        public string InvitedUserId { get; set; }
        public int EventId { get; set; }
    }
    public class InvitationReadDto 
    {
        public UserRefDto InvitingUser { get; set; }
        public UserRefDto InvitedUser { get; set; }
        public EventRefDto EventRef { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Status Status { get; set; }
    }
}

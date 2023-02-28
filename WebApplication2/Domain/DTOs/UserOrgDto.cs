using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class UserOrgCreateDto
    {
        public string UserId { get; set; }
        public int OrginizationId { get; set; }
        public Role role { get; set; }
    }
}

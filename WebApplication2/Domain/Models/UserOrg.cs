using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Domain.Models
{
    [PrimaryKey("UserId", "OrginizationId")]
    public class UserOrg
    {
       
        public string UserId { get; set; }
        public User User { get; set; }
        public int OrginizationId { get; set; }
        public Orginization Orginization { get; set; }
        public OrgRole Role { get; set; }
    }
    public enum OrgRole
    {
        Creator,
        Administrator,
        Member
    }
}

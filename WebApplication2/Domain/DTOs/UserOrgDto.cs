using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class UserOrgCreateDto
    {
        public string UserId { get; set; }
        public int OrginizationId { get; set; }
        public OrgRole role { get; set; }
    }
    public class UserOrgReadDto
    {
        
        public OrganizationRefDto Orginization { get; set; }
        public OrgRole Role { get; set; }
    }
    public class OrgUserReadDto
    {
        public UserRefDto user { get; set; }
        
        public OrgRole Role { get; set; }
    }
}

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
        public int Id { get; set; }
        public UserRefrenceDto user { get; set; }
        public OrganizationRefDto organization { get; set; }
        public OrgRole role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class OrginizationReadDtos
    {
        public int Id { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }

        public string InstagramLink { get; set; }
        public string phoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public ICollection<UserOrgReadDto> UserOrgs { get; set; }
        public ICollection<EventReadDto> Events { get; set; }
    }
    public class OrganizationRefDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
    }
    public class OrginizationCreateDto
    {
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string phoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
       
    
    }
}

using System.ComponentModel.DataAnnotations;
using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class OrginizationReadDtos
    {
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }

        public string InstagramLink { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public ICollection<UserOrgCreateDto> UserOrgs { get; set; }
        public ICollection<EventReadDto> Events { get; set; }
    }
    public class OrginizationCreateDto
    {
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }

        public string InstagramLink { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
       
    
    }
}

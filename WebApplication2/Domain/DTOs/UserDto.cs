using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class UserRegisterDto:UserLoginDto
    {
     
        public string phoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public int countryId { get; set; }
        public int cityId { get; set; }
    }
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
    public class UserRefrenceDto 
    {
        public string id { get; set; }
        public string userName { get; set; }
    }

    public class UserReadDto
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CountryReadDto country { get; set; }
        public CityReadDto city { get; set; }
        public ICollection<UserEventReadDto> EventUsers { get; set; }
        public ICollection<UserOrgReadDto> UserOrgs { get; set; }

    }
}

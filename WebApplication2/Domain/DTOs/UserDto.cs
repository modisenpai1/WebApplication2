using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class UserCreateDto
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int countryId { get; set; }
        public int cityId { get; set; }
    }
    public class UserRefrenceDto 
    {
        public string id { get; set; }
        public string userName { get; set; }
    }

    public class UserReadDto
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CountryReadDto country { get; set; }
        public CityReadDto city { get; set; }
        public ICollection<UserEventDto> EventUsers { get; set; }
        //public ICollection<UserOrg> UserOrgs { get; set; }

    }
}

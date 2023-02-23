using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class UserCreatDto
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CountryReadDto country { get; set; }
        public CityReadDto city { get; set; }
    }
    public class UserReadDto
    {
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CountryReadDto country { get; set; }
        public CityReadDto city { get; set; }
    }
}

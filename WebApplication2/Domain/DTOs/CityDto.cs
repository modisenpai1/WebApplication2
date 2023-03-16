using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Domain.DTOs
{
    public class CityCreateDto
    {
        public string CityName { get; set; }
        public int CountryId { get; set; }

    }
    public class CityReadDto
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public CountryRefDto country { get; set; }
        public ICollection<UserRefrenceDto> UserInCity { get; set; }
        public ICollection<EventRefDto> eventsAtCity { get; set; }

    }
    public class CityRefDto
    {
        public int Id { get; set; }
        public string CityName { get; set; }

    }
}

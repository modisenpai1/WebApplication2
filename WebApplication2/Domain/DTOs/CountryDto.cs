using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Domain.DTOs
{
    public class CountryCreateDto
    {
        public string Name { get; set; }
    }
    public class CountryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CityRefDto> Cities { get; set; }
        public ICollection<UserReadDto> users { get; set; }

    }
    public class CountryRefDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

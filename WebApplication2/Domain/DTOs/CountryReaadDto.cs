using System.ComponentModel.DataAnnotations;
using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class CountryReadDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CityReadDtoCityCountry> Cities { get; set; }
        public ICollection<User> UsersInCountry { get; set; }

    }
}

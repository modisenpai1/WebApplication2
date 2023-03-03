using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class CityReadDto
    {
        [Key]
        public int Id { get; set; }
        public string CityName { get; set; }
        public CountryReadDtoCity country { get; set; }
        public ICollection<UserRefrenceDto> UserInCity { get; set; }
        public ICollection<EventRefDto> eventsAtCity { get; set; }

    }
}

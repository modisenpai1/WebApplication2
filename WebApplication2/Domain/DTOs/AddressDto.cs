using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class AddressCreateDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string Adress1 { get; set; }
        public string? AdditionalInfo { get; set; }
        public bool isActive { get; set; }
        public string Location { get; set; }
        public int OrginizationId { get; set; }

    }
    public class AddressRefDto
    {
        public int Id { get; set; }
        public string Adress1 { get; set; }
        public string AdditionalInfo { get; set; }

    }
    //useless
    public class AddressReadDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string Adress1 { get; set; }
        public string AdditionalInfo { get; set; }
        public bool isActive { get; set; }
        public string Location { get; set; }
        public ICollection<Event> Events { get; set; }
        public int OrginizationId { get; set; }
        public Orginization Orginization { get; set; }

    }
}

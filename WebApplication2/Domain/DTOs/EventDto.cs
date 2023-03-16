using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class EventCreateDto
    {
        public string Title { get; set; }
        public string? ImgPath { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public Accessibality accessibality { get; set; }
        public bool IsOnline { get; set; }
        public string? Link { get; set; }
        public bool hasMaxCap { get; set; }
        public int? MaxCap { get; set; }
        public int AdressId { get; set; }
        public int OrginizationId { get; set; }
        public int CityId { get; set; }
    }

    public class EventReadDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImgPath { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public Accessibality accessibality { get; set; }
        public bool IsOnline { get; set; }
        public string Link { get; set; }
        public bool hasMaxCap { get; set; }
        public int MaxCap { get; set; }
        public ICollection<EventUserReadDto> EventUsers { get; set; }
        public AddressRefDto Adress { get; set; }
        public OrganizationRefDto Orginization { get; set; }
        public CityReadDto City { get; set; }
    }
    public class EventRefDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImgPath { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public Accessibality accessibality { get; set; }
    }



}

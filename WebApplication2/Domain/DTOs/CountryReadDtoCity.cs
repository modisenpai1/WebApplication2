using System.ComponentModel.DataAnnotations;
using WebApplication2.Domain.Models;

namespace WebApplication2.Domain.DTOs
{
    public class CountryReadDtoCity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

      
    }
}

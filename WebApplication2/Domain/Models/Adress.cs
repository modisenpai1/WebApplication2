using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Data;

namespace WebApplication2.Domain.Models
{
    public class Adress : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Adress1 { get; set; }
        public string AdditionalInfo { get; set; }
        public bool isActive { get; set; } 
        public string Location { get; set; }
        public ICollection<Event> Events { get; set; }
        public int OrginizationId { get; set; }
        [ForeignKey("OrginizationId")]
        public Orginization Orginization { get; set; }

        public Adress()
        {
            Events=new List<Event>();
        }

        



    }
}

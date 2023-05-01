using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Domain.Models
{
    public partial class User : IdentityUser
    {
      


        public DateTime BirthDate { get; set; }
       
        public string ImagePath { get; set; }
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City city { get; set; }
        public ICollection<Invitation> EventInvitations { get; set; }
        public ICollection<EventUser> EventUsers { get; set; }
        public ICollection<UserOrg> UserOrgs { get; set; }




        public User()
        {
            EventUsers = new List<EventUser>();
            EventInvitations=new List<Invitation>();
            UserOrgs = new List<UserOrg>();
        }
    }
    
}

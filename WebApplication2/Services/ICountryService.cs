using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface ICountryServices 
    {
        void AddCountry(Country country);
        Country GetCountry(int id);
        
    }
}


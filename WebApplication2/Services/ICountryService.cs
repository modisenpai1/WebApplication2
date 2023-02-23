using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface ICountryServices 
    {
        void AddCountry(Country country);
        Country GetCountry(int id);
        IEnumerable<Country> GetAllCountries();
        public bool SaveChanges();
        public void Update(Country country);
        public void Delete(Country country);

    }
}


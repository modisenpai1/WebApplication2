using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public class CountryServices : ICountryServices
    {
        private readonly IAppRepo<Country> _repo;

        public CountryServices(IAppRepo<Country> repo)
        {
            _repo = repo;
        }

        public void AddCountry(Country country)
        {
            _repo.AddItem(country);
            _repo.SaveChanges();
        }

        public Country GetCountry(int id)
        {
            return _repo.Table.Include(X => X.Cities).FirstOrDefault(x => x.Id == id);
        }
    }
}

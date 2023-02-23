using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IEnumerable<Country> GetAllCountries() {
            return _repo.Table.Include(x => x.Cities).ToList();
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

        public bool SaveChanges()
        {
          return _repo.SaveChanges();
        }

        public void Update(Country country)
        {
           _repo.UpdateItem(country);
        }

        public void Delete(Country country)
        {
           _repo.DeleteItem(country);
        }
    }
}

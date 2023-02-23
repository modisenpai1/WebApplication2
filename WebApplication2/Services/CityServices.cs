using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public class CityServices : ICityServices

    {
        private readonly IAppRepo<City> _repo;

        public CityServices(IAppRepo<City> repo) 
        {
            _repo=repo;
           
        }

        public void AddCity(City city)
        {
           _repo.AddItem(city);
            _repo.SaveChanges();
        }

        public void Delete(City city)
        {
            _repo.DeleteItem(city);
            _repo.SaveChanges();
        }

        public IEnumerable<City> GetAll()
        {
            return _repo.Table.Include(x=>x.country).ToList();
        }

        public City GetCity(int id)
        {   
            
            var city = _repo.Table.Include(X => X.country).FirstOrDefault(x => x.Id == id);
      
            return city;
        }

        public void Update(City city)
        {
            _repo.UpdateItem(city);
            _repo.SaveChanges();
        }
    }
}

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

        public City GetCity(int id)
        {   
            
            var city = _repo.Table.Include(X => X.country).FirstOrDefault(x => x.Id == id);
      
            return city;
        }
    }
}

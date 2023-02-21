using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface ICityServices 
    {
        public void AddCity(City city);
        public City GetCity(int id);
    }
}

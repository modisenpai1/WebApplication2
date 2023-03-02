using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface IOrgenizationServices
    {
        void addOrgenization(Orginization orginization);
        void removeOrgenization(Orginization orginization);
        void updateorginzation(Orginization orginization);
        IEnumerable<Orginization> getAllorginizations();
        Orginization GetOrginzation(int id);
        bool SaveChanges();




    }
} 

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebApplication2.Data;
using WebApplication2.Domain.Models;
namespace WebApplication2.Services
{
    public class OrgenizationServices : IOrgenizationServices
    {
        private readonly IAppRepo<Orginization> _repo;

        public OrgenizationServices(IAppRepo<Orginization> repo)
        {
            _repo = repo;
        }
        public void addOrgenization(Orginization orginization)
        {
            _repo.AddItem(orginization);
            _repo.SaveChanges();
        }

        public IEnumerable<Orginization> getAllorginizations()
        {
           return _repo.Table.Include(x => x.Events).Include(x => x.UserOrgs).Include(x => x.Adresses).ToList();
        }

        public Orginization GetOrginzation(int id)
        {
            return _repo.Table.Include(x => x.Events).Include(x => x.UserOrgs).Include(x => x.Adresses).FirstOrDefault(x => x.Id == id);
        }

     

        public void removeOrgenization(Orginization orginization)
        {
            _repo.DeleteItem(orginization);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }

        public void updateorginzation(Orginization orginization)
        {
            _repo.UpdateItem(orginization);
        }
    }
}

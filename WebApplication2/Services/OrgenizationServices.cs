using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics.Metrics;
using WebApplication2.Data;
using WebApplication2.Domain.Models;
namespace WebApplication2.Services
{
    public class OrgenizationServices : IOrgenizationServices
    {
        private readonly IAppRepo<Orginization> _repo;
        private readonly IAppRepo<UserOrg> _UserOrgrepo;

        public OrgenizationServices(IAppRepo<Orginization> repo, IAppRepo<UserOrg> userOrgrepo)
        {
            _repo = repo;
            _UserOrgrepo = userOrgrepo;
        }

        public void addOrgenization(Orginization orginization)
        {
            _repo.AddItem(orginization);
            _repo.SaveChanges();
        }

        public void AddOrgUser(UserOrg userOrg)
        {
            _UserOrgrepo.AddItem(userOrg);
        }

        public IEnumerable<Orginization> getAllorginizations()
        {
           return _repo.Table.Include(x => x.Events).Include(x => x.UserOrgs).Include(x => x.Adresses).ToList();
        }

        public Orginization GetOrginzation(int id)
        {
            return _repo.Table.Include(x => x.Events).Include(x => x.UserOrgs).Include(x => x.Adresses).FirstOrDefault(x => x.Id == id);
        }

        public UserOrg GetOrgUser(int orgId,string userId)
        {
            return _UserOrgrepo.Table.FirstOrDefault(x => x.UserId == userId && x.OrginizationId == orgId);
        }

        public void removeOrgenization(Orginization orginization)
        {
            _repo.DeleteItem(orginization);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges() && _UserOrgrepo.SaveChanges();
        }

        public void updateorginzation(Orginization orginization)
        {
            _repo.UpdateItem(orginization);
        }

        public void UpdateRole(UserOrg userOrg)
        {
            _UserOrgrepo.UpdateItem(userOrg);
            _UserOrgrepo.SaveChanges();

        }
    }
}

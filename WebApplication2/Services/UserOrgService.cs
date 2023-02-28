using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public class UserOrgService : IUserOrgService
    {
        private readonly IAppRepo<UserOrg> _repo;

        public UserOrgService(IAppRepo<UserOrg> repo) { _repo = repo; }
        public void AddUserOrg(UserOrg userOrg)
        {
            _repo.AddItem(userOrg);
            _repo.SaveChanges();
        }

        public void Delete(UserOrg userOrg)
        {
            _repo.DeleteItem(userOrg);
            _repo.SaveChanges();
        }

        public UserOrg GetUserOrg(int id)
        {
            return _repo.GetById(id);
        }

        public void Update(UserOrg userOrg)
        {
            _repo.UpdateItem(userOrg);
            _repo.SaveChanges();
        }
    }
}

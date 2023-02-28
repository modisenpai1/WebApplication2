using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface IUserOrgService
    {
        public UserOrg GetUserOrg(int id);
        public void AddUserOrg(UserOrg userOrg);
        public void Update(UserOrg userOrg);
        public void Delete(UserOrg userOrg);
    }
}

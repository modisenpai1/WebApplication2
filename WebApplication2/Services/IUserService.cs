using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface IUserService
    {
        public void AddUser(User user);
        public User GetUser(string key);
        public IEnumerable<User> GetAll();
        public void Update(User user);
        public void Delete(User user);
    }
}

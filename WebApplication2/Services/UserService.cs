using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public class UserService : IUserService
    {
        private readonly IAppRepo<User> _repo;

        public UserService(IAppRepo<User> repo)
        {
            _repo = repo;
        }
        public void AddUser(User user)
        {
            _repo.AddItem(user);
            _repo.SaveChanges();
        }

        public void Delete(User user)
        {
            _repo.DeleteItem(user);
            _repo.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _repo.Table.Include(x =>x.city).
                Include(x=>x.UserOrgs).
                Include(x=>x.EventUsers).
                ToList();
        }

        public User GetUser(string key)
        {
            return _repo.Table.Include(x => x.city).
                Include(x => x.UserOrgs).
                Include(x => x.EventUsers).FirstOrDefault(x=>x.Id==key);
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}

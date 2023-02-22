using WebApplication2.Domain.Models;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Data
{
    public class AppRepo<TEntity>: IAppRepo<TEntity> where TEntity : class,IEntity

    {
        private readonly AppDbContext _context;

        public AppRepo(AppDbContext context)
        {
            _context = context;

        }

        public IQueryable<TEntity> Table =>_context.Set<TEntity>();

        public void AddItem(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentException(nameof(item));
            }
            _context.Set<TEntity>().Add(item);
        }

        public void DeleteItem(TEntity item)
        {
           _context.Set<TEntity>().Remove(item);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {

            return _context.SaveChanges() >= 0;
        }

     

        public void UpdateItem(TEntity item)
        {
           
        }

      
    }
}

using WebApplication2.Domain.Models;

namespace WebApplication2.Data
{
    public interface IAppRepo<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void AddItem(TEntity item);
        //used to require the id with the item in the update method 
        //removed as i didnt see the point of including the id with it
        void UpdateItem(TEntity item);
        bool SaveChanges();
        void DeleteItem(TEntity item);
        IQueryable<TEntity> Table { get; }

    }
}

using WebApplication2.Domain.Models;

namespace WebApplication2.Data
{
    public interface IAppRepo<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void AddItem(TEntity item);
        void UpdateItem(TEntity item,int id);
        bool SaveChanges();
        void DeleteItem(TEntity item);
        IQueryable<TEntity> Table { get; }

    }
}

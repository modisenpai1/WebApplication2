using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public class EventService : IEventService
    {
        private readonly IAppRepo<Event> _repo;

        public EventService(IAppRepo<Event> repo)
        {
            _repo = repo;
        }
        public void AddEvent(Event evnt)
        {
            _repo.AddItem(evnt);
            _repo.SaveChanges();
        }

        public void Delete(Event evnt)
        {
            _repo.DeleteItem(evnt);
            _repo.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            return _repo.Table.Include(x => x.Adress)
                .Include(x => x.City)
                .Include(x=>x.Country)
                .Include(x => x.EventUsers)
                .Include(x => x.Orginization)
                .ToList();
        }

        public Event GetEvent(int id)
        {
            return _repo.Table.Include(x => x.Adress)
                .Include(x => x.City)
                .ThenInclude(y=>y.country)
                .Include(x => x.EventUsers)
                .Include(x => x.Orginization)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Event> Search(string name,City city)
        {
            IQueryable<Event> Query = _repo.Table.Include(x => x.Adress)
                 .Include(x => x.City)
                 .ThenInclude(y => y.country)
                 .Include(x => x.EventUsers)
                 .Include(x => x.Orginization)
                 ;
            if(!string.IsNullOrEmpty(name))
            {
                Query = Query.Where(e=>e.Title== name);
            }
            if(city!=null)
            {
                Query = Query.Where(e=>e.City== city);
            }
            return Query.ToList();
        }

        public void Update(Event evnt)
        {
            _repo.UpdateItem(evnt);
            _repo.SaveChanges();
        }
    }
}

using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface IEventService
    {
        public void AddEvent(Event evnt);
        public Event GetEvent(int id);
        public IEnumerable<Event> GetAll();
        public IEnumerable<Event> Search(string name,City city);
        public void Update(Event evnt);
        public void Delete(Event evnt);
        //event User
        void Enroll(EventUser eventUser);
        void UpdateRole(EventUser eventUser);
        EventUser GetEventUser(int EventId, string userId);
        IEnumerable<EventUser> GetEventUsers(int EventId);
        void DeleteEventUser(EventUser eventUser);
    }
}

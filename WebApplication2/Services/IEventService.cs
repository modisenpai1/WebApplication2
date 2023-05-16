using WebApplication2.Domain.DTOs;
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
        public IEnumerable<Event> Search(EventSearchDto eventSearchDto);
        //event User
        void AddEventUser(EventUser eventUser);
        void UpdateEventUser(EventUser eventUser);
        EventUser GetEventUser(int EventId, string userId);
        IEnumerable<EventUser> GetEventUsers(int EventId);
        void DeleteEventUser(EventUser eventUser);
        //invitatoin services
        public void AddInvitation(Invitation invitation);
        public void DeleteInvitation(Invitation invitation);
        public void UpdateInvitation(Invitation invitation);
        public Invitation GetInvitationById(int Id);
        public IEnumerable<Invitation> GetInvitationsByUser(string UserId);
        public IEnumerable<Invitation> GetInvitationsForUser(string UserId);
        public IEnumerable<Invitation> GetEventInvitations(int EventId);

    }
}

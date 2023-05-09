using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebApplication2.Data;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public class EventService : IEventService
    {
        private readonly IAppRepo<Event> _repo;
        private readonly IAppRepo<EventUser> _eventUserRepo;
        private readonly IAppRepo<Invitation> _InvitationRepo;

        public EventService(IAppRepo<Event> repo, IAppRepo<EventUser> eventUserRepo, IAppRepo<Invitation> invitationRepo)
        {
            _repo = repo;
            _eventUserRepo = eventUserRepo;
            _InvitationRepo = invitationRepo;
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
                .Include(x => x.Country)
                .Include(x => x.EventUsers)
                .Include(x => x.Orginization)
                .ToList();
        }

        public Event GetEvent(int id)
        {
            return _repo.Table.Include(x => x.Adress)
                .Include(x => x.City)
                .ThenInclude(y => y.country)
                .Include(x => x.EventUsers)
                .Include(x => x.Orginization)
                .FirstOrDefault(x => x.Id == id);
        }



        public IEnumerable<Event> Search(string name, City city)
        {
            IQueryable<Event> Query = _repo.Table.Include(x => x.Adress)
                 .Include(x => x.City)
                 .ThenInclude(y => y.country)
                 .Include(x => x.EventUsers)
                 .Include(x => x.Orginization)
                 ;
            if (!string.IsNullOrEmpty(name))
            {
                Query = Query.Where(e => e.Title == name);
            }
            if (city != null)
            {
                Query = Query.Where(e => e.City == city);
            }
            return Query.ToList();
        }

        public void Update(Event evnt)
        {
            _repo.UpdateItem(evnt);
            _repo.SaveChanges();
        }
        //EventUser___________________________________________________________________________________________
        public void UpdateEventUser(EventUser eventUser)
        {
            _eventUserRepo.UpdateItem(eventUser);
            _eventUserRepo.SaveChanges();
        }
        public void DeleteEventUser(EventUser eventUser)
        {
            _eventUserRepo.DeleteItem(eventUser);
            _eventUserRepo.SaveChanges();
        }

        public void AddEventUser(EventUser eventUser)
        {
            _eventUserRepo.AddItem(eventUser);
            _eventUserRepo.SaveChanges();
        }
        public EventUser GetEventUser(int EventId, string userId)
        {
            return _eventUserRepo.Table.FirstOrDefault(x => x.EventId == EventId && x.UserId == userId);
        }

        public IEnumerable<EventUser> GetEventUsers(int EventId)
        {
            return _eventUserRepo.Table.Include(x => x.User).ToList();
        }




        // invitation service__________________________________________________________________________________
        public void AddInvitation(Invitation Invitation) 
        {
            Invitation.ExpirationDate = DateTime.Now.AddDays(3);
            _InvitationRepo.AddItem(Invitation);
            _InvitationRepo.SaveChanges();
        }
        public void DeleteInvitation(Invitation Invitation) 
        {
            _InvitationRepo.DeleteItem(Invitation); 
            _InvitationRepo.SaveChanges();
        }
        public void UpdateInvitation(Invitation Invitation) 
        {
            _InvitationRepo.UpdateItem(Invitation);
            _InvitationRepo.SaveChanges();
        }
        public IEnumerable<Invitation> GetInvitationsByUser(string UserId) 
        {
            var Invitations = _InvitationRepo.Table.
                Where(x => x.InvitingUserId == UserId).
                Include(x => x.InvitingUser).
                Include(x => x.InvitedUser).
                Include(x => x.Event).
                ToList();
            foreach (var invitation in Invitations)
            {
                if (invitation.Status == Status.Pending && invitation.ExpirationDate < DateTime.Now)
                {
                    invitation.Status = Status.Expired;
                }
            }
            return Invitations;
            
        }
        public IEnumerable<Invitation> GetInvitationsForUser(string UserId) 
        {
            var Invitations = _InvitationRepo.Table.
                Where(x => x.InvitedUserId == UserId).
                Include(x => x.InvitingUser).
                Include(x => x.InvitedUser).
                Include(x => x.Event).
                ToList();
            foreach (var invitation in Invitations)
            {
                if (invitation.Status == Status.Pending && invitation.ExpirationDate < DateTime.Now)
                {
                    invitation.Status = Status.Expired;
                }
            }
            return Invitations;
        }
        public IEnumerable<Invitation> GetEventInvitations(int EventId) 
        {
            var invitations= _InvitationRepo.Table.
               Where(x => x.EventId == EventId).
               Include(x => x.InvitingUser).
               Include(x => x.InvitedUser).
               Include(x => x.Event).
               ToList();
            foreach (var invitation in invitations)
            {
                if(invitation.Status == Status.Pending && invitation.ExpirationDate<DateTime.Now)
                {
                    invitation.Status= Status.Expired;
                }
            }
            return invitations;
        }
        public Invitation GetInvitationById(int Id)
        {
            var Invitation = _InvitationRepo.Table.
               Include(x => x.InvitingUser).
               Include(x => x.InvitedUser).
               Include(x => x.Event).
               FirstOrDefault(x => x.Id == Id);
            if (Invitation.Status == Status.Pending && Invitation.ExpirationDate < DateTime.Now)
            {
                Invitation.Status = Status.Expired;
            }
            
            return Invitation;
            


        }

        //get invitation for user

    }
}
